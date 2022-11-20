using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Claims;
using TravelMate.Core.Contracts;
using TravelMate.Core.Services;

namespace TravelMate.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IUserService userService;

        public FriendsController(IUserService _userService)
        {
            this.userService = _userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            var model = await userService.GetAllUsers();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllFriends()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await userService.GetAllFriends(userId);

            return View("AllUsers", model);
        }

        [HttpPost]
        //public async IActionResult SendRequest(string friendId)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    await 

        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        public async Task<IActionResult> AddFriend(string friendId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            await userService.AddFriend(userId, friendId);

            return RedirectToAction("Index", "Home");
        }
    }
}
