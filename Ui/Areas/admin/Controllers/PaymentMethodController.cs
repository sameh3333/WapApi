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
    public class PaymentMethodController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPaymentMethod _paymendMethod;
        public PaymentMethodController(IPaymentMethod paymendMethodntry)
        {

            _paymendMethod = paymendMethodntry;

        }
        public IActionResult List()
        {
            var getdata = _paymendMethod.GetAll();
            return View(getdata);
        }
        public IActionResult Edit(Guid? Id)
        {
            var data = new BL.DTOs.PaymentMethodDTOs();
            if (Id != null)
                data = _paymendMethod.GetById((Guid)Id);
            return View(data);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(PaymentMethodDTOs data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            {
                if (data.Id == Guid.Empty)
                    _paymendMethod.Add(data);
                else
                    _paymendMethod.Update(data, data.Id);
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
                _paymendMethod.ChangeStatus(id, Guid.NewGuid());
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
