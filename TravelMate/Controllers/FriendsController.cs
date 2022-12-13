using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Constants;
using TravelMate.Core.Contracts;
using TravelMate.Extension;

namespace TravelMate.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IFriendService friendService;

        public FriendsController(IFriendService _friendService)
        {
            this.friendService = _friendService;
        }

        public async Task<IActionResult> AddFriend(string id)
        {
            var userId = User.Id();

            if (await friendService.UsersAreFriends(userId, id))
            {
                TempData[MessageConstants.ErrorMessage] = "You are already friends";

                return RedirectToAction("ViewProfile", "Profile", new { @id = id });
            }

            if (userId == id)
            {
                TempData[MessageConstants.ErrorMessage] = "You cannot add yourseld as a friend";

                return RedirectToAction("ViewProfile", "Profile", new { @id = id });
            }

            await friendService.AddFriend(userId, id);

            TempData[MessageConstants.SuccessMessage] = "Successfully added to friends";

            return RedirectToAction("ViewProfile", "Profile", new { @id = id });
        }

        public async Task<IActionResult> ViewAllFriends(string id)
        {
            var model = await friendService.GetAllFriends(id);

            return View(model);
        }
    }
}
