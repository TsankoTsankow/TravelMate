using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Constants;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Post;
using TravelMate.Extension;

namespace TravelMate.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly ICountryService countryService;
        private readonly ICategoryService categoryService;
        private readonly ILikeService likeService;
        private readonly INotificationService notificationService;
        private readonly IPhotoService photoService;

        public PostController(
            IPostService _postService,
            ICountryService _countryService,
            ICategoryService _categoryService,
            ILikeService _likeService,
            INotificationService _notificationService,
            IPhotoService _photoService)
        {
            this.postService = _postService;
            this.countryService = _countryService;
            this.categoryService = _categoryService;
            this.likeService = _likeService;
            this.notificationService = _notificationService;
            this.photoService = _photoService;
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
            var url = await photoService.UploadPhoto(post.File);

            try
            {
                await postService.CreatePost(post, userId, url);

                TempData[MessageConstants.SuccessMessage] = "Post successfully added!";

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

            var url = await photoService.UploadPhoto(model.File);

            await postService.Edit(model, model.Id, url);

            TempData[MessageConstants.SuccessMessage] = "Post successfully edited!";

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

            TempData[MessageConstants.SuccessMessage] = "Post successfully deleted!";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AllPosts([FromQuery] AllPostsQueryModel query)
        {
            var result = await postService.GetAllPostsQuery(query.Category, query.Country);

            query.Categories = await categoryService.GetAllCategoriesNames();
            query.Countries = await countryService.GetAllCountiresNames();
            query.Posts = result.Posts;

            return View(query);
        }

        public async Task<IActionResult> AllPostsOfUser(string id)
        {
            var model = await postService.GetAllPostsByUserId(id);

            return View(model);
        }
    }
}
