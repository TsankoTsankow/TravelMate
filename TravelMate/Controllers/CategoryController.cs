using Microsoft.AspNetCore.Mvc;
using TravelMate.Core.Contracts;
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

    }
}
