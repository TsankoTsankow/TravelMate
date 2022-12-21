using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.CountryModels;

namespace TravelMate.Controllers
{
    [Authorize(Roles = "TravelGuru")]

    public class CountryController : Controller
    {
        private readonly ICountryService countryService;
        public CountryController(ICountryService _countryService)
        {
            this.countryService = _countryService;
        }

        public async Task<IActionResult> AllCountries()
        {
            var model = await countryService.GetAllCountries();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new EditCountryViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EditCountryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            await countryService.Add(model);

            return RedirectToAction("AllCountries");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await countryService.GetCountryById(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCountryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            await countryService.Edit(model);

            return RedirectToAction("AllCountries");
        }
    }
}
