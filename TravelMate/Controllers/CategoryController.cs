using Microsoft.AspNetCore.Mvc;

namespace TravelMate.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
