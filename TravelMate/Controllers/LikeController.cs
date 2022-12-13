using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Constants;
using TravelMate.Core.Contracts;
using TravelMate.Extension;

namespace TravelMate.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeService likeService;
        private readonly INotificationService notificationService;

        public LikeController(
            ILikeService _likeService,
            INotificationService _notificationService)
        {
            this.likeService = _likeService;
            this.notificationService = _notificationService;
        }
        public async Task<IActionResult> LikePost(int id)
        {
            var userId = User.Id();

            if (await likeService.UserLikedPost(id, userId))
            {
                TempData[MessageConstants.WarningMessage] = "Post is already liked!";

                return RedirectToAction("Index", "Home");
            }

            await likeService.AddLike(id, userId);

            TempData[MessageConstants.SuccessMessage] = "Post liked!";

            await notificationService.SendLikeNotification(id, userId);

            return RedirectToAction("Index", "Home");
        }
    }
}
