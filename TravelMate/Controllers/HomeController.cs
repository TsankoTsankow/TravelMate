using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Photo;
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await postService.GetAllPostsById(userId);

            return View(model);
        }

        

        //[HttpGet]
        //public IActionResult AddPhoto()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddPhoto([FromForm] AddPhotoViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction(nameof(AddPhoto));
        //    }

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    //await postService.AddPhotoToFolder(model, userId);

        //    return RedirectToAction(nameof(Gallery));
        //}

        //[HttpGet]
        //public async Task<IActionResult> Gallery()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var model = await postService.DisplayUserGallery(userId);

        //    return View(model);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}