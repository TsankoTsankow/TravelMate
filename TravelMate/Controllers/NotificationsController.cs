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

        public async Task<IActionResult> SendRequest(string id)
        {
            string userId = User.Id();

            if (await notificationService.UsersAreFriends(userId, id))
            {
                //throw new Exception("User is already a friend");
                return RedirectToAction("ViewProfile", "Profile", new {@id = id});
            }

            await notificationService.SendFriendRequest(userId, id);

            return RedirectToAction("ViewProfile", "Profile", new { @id = id });
        }

        

    }
}
