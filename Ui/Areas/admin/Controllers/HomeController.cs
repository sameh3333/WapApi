using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.ControllersControllers
{
    [Area("admin")]
    [Authorize]


    [Authorize(Roles = "Admin")]


    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
