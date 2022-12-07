using Microsoft.AspNetCore.Mvc;

namespace TravelMate.Controllers
{
    public class RegionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
