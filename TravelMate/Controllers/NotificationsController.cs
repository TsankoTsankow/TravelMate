using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TravelMate.Core.Contracts;
using TravelMate.Extension;

namespace TravelMate.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService notificationService;

        public NotificationsController(INotificationService _notificationService)
        {
            this.notificationService = _notificationService;
        }


        public async Task<IActionResult> Index()
        {
            var model = await notificationService.GetNotificationsByUserId(User.Id());

            return View(model);
        }

        public async Task<IActionResult> SendRequest(string id)
        {
            string userId = User.Id();

            if (await notificationService.UsersAreFriends(userId, id))
            {
                //throw new Exception("User is already a friend");
                return RedirectToAction("ViewProfile", "Profile", new {@id = id});
            }

            if (userId == id)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await notificationService.SendFriendRequest(userId, id);

            return RedirectToAction("ViewProfile", "Profile", new { @id = id });
        }

        public async Task<IActionResult> AddFriend(string id)
        {
            var userId = User.Id();

            if (await notificationService.UsersAreFriends(userId, id))
            {
                //throw new Exception("User is already a friend");
                return RedirectToAction("ViewProfile", "Profile", new { @id = id });
            }

            if (userId == id)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await notificationService.AddFriend(userId, id);

            return RedirectToAction("ViewProfile", "Profile", new { @id = id });
        }
        

    }
}
