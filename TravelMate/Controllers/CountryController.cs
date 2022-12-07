using Microsoft.AspNetCore.Mvc;

namespace TravelMate.Controllers
{
    public class CountryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
