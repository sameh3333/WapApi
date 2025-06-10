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
    public class SettingController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ISetting _Setting ;
        public SettingController(ISetting Setting )
        {

          _Setting = Setting;

        }
        public IActionResult List()
        {
            var getdata = _Setting.GetAll();
            return View(getdata);
        }
        public IActionResult Edit(Guid? Id)
        {
            var data = new BL.DTOs.SettingDTOs();
            if (Id != null)
                data = _Setting.GetById((Guid)Id);
            return View(data);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(SettingDTOs data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            {
                if (data.Id == Guid.Empty)
                    _Setting.Add(data);
                else
                    _Setting.Update(data, data.Id);
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
                _Setting.ChangeStatus(id, Guid.NewGuid());
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
