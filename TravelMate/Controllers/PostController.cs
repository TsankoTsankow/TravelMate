using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Post;
using TravelMate.Core.Services;

namespace TravelMate.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService _postService)
        {
            this.postService = _postService;
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            var model = new PostViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromForm] PostViewModel post)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AddPost));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await postService.CreatePost(post, userId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(post);
            }

        }
    }
}
