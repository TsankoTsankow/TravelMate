using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static TravelMate.Areas.Admin.Constants.AdminConstants;

namespace TravelMate.Areas.Admin.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdminRoleName)]
    [Route("Admin/[Controller]/[Action]/{id?}")]
    public class BaseController : Controller
    {
       
    }
}
