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
        public ActionResult<ApiResponse<List<PaymentMethodDTOs>>> Get()
        {
            try 
            {

                var Data = _IPaymentMethod.GetAll();

                return Ok(ApiResponse<List<PaymentMethodDTOs>>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<List<PaymentMethodDTOs>>.FailResponse
                    ("Data Access Exception ",new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<PaymentMethodDTOs>>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }


        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<PaymentMethodDTOs>> Get(Guid id)
        {
            try
            {

                var Data = _IPaymentMethod.GetById(id);

                return Ok(ApiResponse<PaymentMethodDTOs>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<PaymentMethodDTOs>.FailResponse
                    ("Data Access Exception ", new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<PaymentMethodDTOs>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }
        }

        // POST api/<ShippingTypesController>
       
    }
}
