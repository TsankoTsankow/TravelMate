using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;
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

        public async Task<IActionResult> ViewProfile()
        {
            var model = await profileService.DisplayProfileById(User.Id());

            return View(model);
        }
    }
}
