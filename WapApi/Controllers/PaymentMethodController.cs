using BL.Contracts;
using BL.DTOs;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WapApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {

        IPaymentMethod _IPaymentMethod;

        public PaymentMethodController(IPaymentMethod IPaymentMethod)
        { 
            _IPaymentMethod= IPaymentMethod;
        
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<ShipingPackgingDTOs>>> Get()
        {
            try 
            {

                var Data = _IPaymentMethod.GetAll();

                return Ok(ApiResponse<List<ShipingPackgingDTOs>>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<List<ShipingPackgingDTOs>>.FailResponse
                    ("Data Access Exception ",new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<ShipingPackgingDTOs>>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }


        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ShipingPackgingDTOs>> Get(Guid id)
        {
            try
            {

                var Data = _IPaymentMethod.GetById(id);

                return Ok(ApiResponse<ShipingPackgingDTOs>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<ShipingPackgingDTOs>.FailResponse
                    ("Data Access Exception ", new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ShipingPackgingDTOs>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }
        }

        // POST api/<ShippingTypesController>
       
    }
}
