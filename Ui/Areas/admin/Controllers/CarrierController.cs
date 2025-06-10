using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ui.Herpers;

namespace Ui.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class CarrierController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICairrer _Cairrer;
        public CarrierController(ICairrer Country)
        {

            _Cairrer = Country;

        }
        public IActionResult List()
        {
            var getdata = _Cairrer.GetAll();
            return View(getdata);
        }
        public IActionResult Edit(Guid? Id)
        {
            var data = new BL.DTOs.CarrierDtos();
            if (Id != null)
                data = _Cairrer.GetById((Guid)Id);
            return View(data);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CarrierDtos data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            {
                if (data.Id == Guid.Empty)
                    _Cairrer.Add(data);
                else
                    _Cairrer.Update(data, data.Id);
                TempData["MessageType"] = MessageType.SaveSucess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageType.SaveFailed;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(Guid id)
        {
            TempData["MessageType"] = null;
            try
            {
                _Cairrer.ChangeStatus(id, Guid.NewGuid());
                TempData["MessageType"] = MessageType.DeleteSucess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageType.DeleteFailed;
            }
            return RedirectToAction("List");
        }
    }

}
