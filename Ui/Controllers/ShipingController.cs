using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class ShipingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
