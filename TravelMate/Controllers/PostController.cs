using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Post;
using TravelMate.Core.Services;
using TravelMate.Extension;

namespace TravelMate.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly ICountryService countryService;
        private readonly ICategoryService categoryService;

        public PostController(
            IPostService _postService,
            ICountryService _countryService,
            ICategoryService _categoryService)
        {
            this.postService = _postService;
            this.countryService = _countryService;
            this.categoryService = _categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> AddPost()
        {

            var categories = await categoryService.GetAllCategories();
            var countries = await countryService.GetAllCountries();

            var model = new CreatePostViewModel()
            {
                Categories = categories,
                Countries = countries
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromForm] CreatePostViewModel post)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AddPost));
            }

            var userId = User.Id();

            try
            {
                await postService.CreatePost(post, userId);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(post);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await postService.GetPostById(id);

            if (model.AuthorId != User.Id())
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var categories = await categoryService.GetAllCategories();
            var countries = await countryService.GetAllCountries();

            model.Categories = categories;
            model.Countries = countries;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            if (model.AuthorId != User.Id())
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await postService.Edit(model, model.Id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await postService.GetPostInfoByPostId(id);

            if (User.Id() != model.AuthorId)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, PostViewModel model)
        {
            
            await postService.Delete(id);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AllPostsByCategory([FromQuery] PostsByCategoryViewModel query)
        {
            var result = await postService.GetAllPostsByCategory(query.Category);

            query.Categories = await categoryService.GetAllCategoriesNames();
            query.Posts = result.Posts;

            return View(query);
        }

        public async Task<IActionResult> AllPostsByCountry([FromQuery] PostsByCountryViewModel query)
        {
            var result = await postService.GetAllPostsByCountry(query.Country);

            query.Countries = await countryService.GetAllCountiresNames();
            query.Posts = result.Posts;

            return View(query);
        }
    }
}
