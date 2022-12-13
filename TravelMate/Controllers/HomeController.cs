using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelMate.Core.Contracts;
using TravelMate.Extension;
using TravelMate.Models;

namespace TravelMate.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService postService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IPostService _postService)
        {
            _logger = logger;
            postService = _postService;
        }


        public async Task<IActionResult> Index()
        {
            if (!User?.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("Login", "User");
            }

            var userId = User.Id();

            var model = await postService.GetAllPostsOfUserFriends(userId);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}