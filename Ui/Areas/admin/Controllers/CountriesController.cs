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
    public class CountriesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICountry _Country;
        public CountriesController(ICountry country)
        {

            _Country = country;

        }
        public IActionResult List()
        {
            var getdata = _Country.GetAll();
            return View(getdata);
        }
        public IActionResult Edit(Guid? Id)
        {
            var data = new BL.DTOs.CountryDTOs();
            if (Id != null)
                data = _Country.GetById((Guid)Id);
            return View(data);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CountryDTOs data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            {
                if (data.Id == Guid.Empty)
                    _Country.Add(data);
                else
                    _Country.Update(data, data.Id);
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
                _Country.ChangeStatus(id, Guid.NewGuid());
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
