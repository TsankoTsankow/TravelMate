using Microsoft.AspNetCore.Mvc;

namespace TravelMate.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult ViewProfile(string Id)
        {
            return View();
        }
    }
}
