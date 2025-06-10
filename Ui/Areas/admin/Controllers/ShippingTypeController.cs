using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ui.Herpers;

 namespace Ui.Areas.Admin.Controllers
{
   [Area("admin")]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class ShippingTypeController : Controller
    {
        private readonly IShippmentType _ShippingType;
        public ShippingTypeController(IShippmentType ShippingTyp) { 
        
            _ShippingType = ShippingTyp;
        
        }
        public IActionResult List()
        {
           var getdata = _ShippingType.GetAll();
             return View(getdata);
        } 
        public IActionResult Edit(Guid? Id )
        {
            var data= new BL.DTOs.ShippingTypeDTOs();
            if (Id != null)
                data = _ShippingType.GetById((Guid)Id);
            return View(data);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult >Save(ShippingTypeDTOs data )
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid) 
               return View("Edit", data);
            try
            {
                if (data.Id == Guid.Empty)
                    _ShippingType.Add(data);
                else
                    _ShippingType.Update(data, data.Id);
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
            try {
                _ShippingType.ChangeStatus(id, Guid.NewGuid());
                TempData["MessageType"] = MessageType.DeleteSucess;
            } catch (Exception ex) {
                TempData["MessageType"]= MessageType.DeleteFailed;  
            }
            return RedirectToAction("List");
        }
    }

}
