using BL.Contracts.Shipment;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class ShipingController : Controller
    {
        private readonly IShippment _shippmnt;

        public ShipingController(IShippment ashipping )
        {
          _shippmnt = ashipping;
 

         }

        public async Task<IActionResult> List()
            {
            var getshipping = await _shippmnt.GetShipments();
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        

    }
}
