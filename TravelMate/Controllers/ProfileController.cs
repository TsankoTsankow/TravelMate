using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Profile;
using TravelMate.Extensions;

namespace TravelMate.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService _profileService)
        {
            this.profileService = _profileService;
        }

        public async Task<IActionResult> ViewProfile(string Id)
        {
            var model = await profileService.DisplayProfileById(Id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = User.Id();

            var model = await profileService.DisplayProfileById(userId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await profileService.Edit(User.Id(), model);

            return RedirectToAction("ViewProfile", "Profile", (User.Id()));
        }
    }
}
