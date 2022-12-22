using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;

namespace TravelMate.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : BaseController
    {
        private readonly IPostService postService;

        public HomeController(IPostService _postService)
        {
            this.postService = _postService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await postService.GetAllPosts();

            return View(model);
        }

        
    }
}
