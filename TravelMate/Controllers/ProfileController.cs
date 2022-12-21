using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Constants;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.Profile;
using TravelMate.Extension;

namespace TravelMate.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;
        private readonly ICountryService countryService;
        private readonly IPhotoService photoService;


        public ProfileController(
            IProfileService _profileService,
            ICountryService _countryService,
            IPhotoService _photoService)
        {
            this.profileService = _profileService;
            this.countryService = _countryService;
            this.photoService = _photoService;
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile(string id)
        {
            var model = await profileService.DisplayProfileById(id);
            
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

            var url = await photoService.UploadPhoto(model.ProfilePicture);

            await profileService.Edit(User.Id(), model, url);

            TempData[MessageConstants.SuccessMessage] = "Profile successfully edited!";

            return RedirectToAction("ViewProfile", "Profile", new {@id = User.Id()});
        }
    }
}
