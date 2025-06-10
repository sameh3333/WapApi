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
    public class CarrierController : ControllerBase
    {

        ICairrer _ICairrer;

        public CarrierController(ICairrer Cairrer)
        { 
            _ICairrer= Cairrer;
        
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<CarrierDtos>>> Get()
        {
            try 
            {

                var Data = _ICairrer.GetAll();

                return Ok(ApiResponse<List<CarrierDtos>>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<List<CarrierDtos>>.FailResponse
                    ("Data Access Exception ",new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<CarrierDtos>>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }


        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<CarrierDtos>> Get(Guid id)
        {
            try
            {

                var Data = _ICairrer.GetById(id);

                return Ok(ApiResponse<CarrierDtos>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<CarrierDtos>.FailResponse
                    ("Data Access Exception ", new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CarrierDtos>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }
        }

        // POST api/<ShippingTypesController>
       
    }
}
