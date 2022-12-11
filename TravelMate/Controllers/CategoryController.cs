using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;
using TravelMate.Core.Models.CategoryModels;
using TravelMate.Core.Models.Post;
using TravelMate.Core.Services;
using TravelMate.Extension;

namespace TravelMate.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(
            ICategoryService _categoryService) 
        {
            this.categoryService = _categoryService;
        }

        public async Task<IActionResult> AllCategories()
        {
            var model = await categoryService.GetAllCategories();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new EditCategoryViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", model);
            }

            await categoryService.Add(model);

            return RedirectToAction("AllCategories");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await categoryService.GetCategoryById(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            await categoryService.Edit(model);

            return RedirectToAction("AllCategories");
        }

    }
}
