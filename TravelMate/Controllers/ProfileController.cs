using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Profile;
using TravelMate.Core.Services;
using TravelMate.Extension;

namespace TravelMate.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;
        private readonly ICountryService countryService;

        public ProfileController(IProfileService _profileService,
            ICountryService _countryService)
        {
            this.profileService = _profileService;
            this.countryService = _countryService;
        }

        public async Task<IActionResult> ViewMyProfile()
        {
            string Id = User.Id();

            var model = await profileService.DisplayProfileById(Id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = User.Id();

            var user = await profileService.DisplayProfileById(userId);
            var countries = await countryService.GetAllCountries();

            var model = new EditProfileViewModel()
            {
                UserId = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                CountryId = user.CountryId,
                Countries = countries,
                Information = user.Information, 

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
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
