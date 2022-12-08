using Microsoft.AspNetCore.Mvc;
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
    }
}
