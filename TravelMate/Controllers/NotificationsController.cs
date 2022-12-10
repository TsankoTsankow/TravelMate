using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;
using TravelMate.Extension;

namespace TravelMate.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly IFriendService friendService;

        public NotificationsController(
            INotificationService _notificationService, 
            IFriendService friendService)
        {
            this.notificationService = _notificationService;
            this.friendService = friendService;
        }


        public async Task<IActionResult> Index()
        {
            var model = await notificationService.GetNotificationsByUserId(User.Id());

            return View(model);
        }

        public async Task<IActionResult> SendFriendRequest(string id)
        {
            string userId = User.Id();

            if (await friendService.UsersAreFriends(userId, id))
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

    }
}
