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
    public class CityController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ICity _City;
        private readonly ICountry _Countrry;
        public CityController(ICity country, ICountry countrry)
        {

            _City = country;
            _Countrry = countrry;
        }
        public IActionResult List()
        {
            var getdata = _City.GetAllCities();
            return View(getdata);
        }
        public IActionResult Edit(Guid? Id)
        {
            var data = new BL.DTOs.CityDTOs();
            LoadCounitrs();
            if (Id != null)
                data = _City.GetById((Guid)Id);
            return View(data);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CityDTOs data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
            {
                LoadCounitrs();
                return View("Edit", data);
            }
            try
            {
                if (data.Id == Guid.Empty)
                    _City.Add(data);
                else
                    _City.Update(data, data.Id);
                TempData["MessageType"] = MessageType.SaveSucess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageType.SaveFailed;
            }
            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            TempData["MessageType"] = null;
            try
            {
                _City.ChangeStatus(id, Guid.NewGuid());
                TempData["MessageType"] = MessageType.DeleteSucess;
            }
            catch (Exception ex)
            {
                TempData["MessageType"] = MessageType.DeleteFailed;
            }
            return RedirectToAction("List");
        }


        void LoadCounitrs()
        {
            var Country = _Countrry.GetAll();
            ViewBag.Countries = Country;
        }
    }

}
