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
    public class ShippingTypesController : ControllerBase
    {

        IShippmentType _shipppingTypes;

        public ShippingTypesController(IShippmentType shipppingTypes)
        { 
            _shipppingTypes=shipppingTypes;
        
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<ShippingTypeDTOs>>> Get()
        {
            try 
            {

                var Data = _shipppingTypes.GetAll();

                return Ok(ApiResponse<List<ShippingTypeDTOs>>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<List<ShippingTypeDTOs>>.FailResponse
                    ("Data Access Exception ",new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<ShippingTypeDTOs>>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }


        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ShippingTypeDTOs>> Get(Guid id)
        {
            try
            {

                var Data = _shipppingTypes.GetById(id);

                return Ok(ApiResponse<ShippingTypeDTOs>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<ShippingTypeDTOs>.FailResponse
                    ("Data Access Exception ", new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<ShippingTypeDTOs>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }
        }

        // POST api/<ShippingTypesController>
       
    }
}
