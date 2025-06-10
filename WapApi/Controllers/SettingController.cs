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
    public class SettingController : ControllerBase
    {

        ISetting _ISetting;

        public SettingController(ISetting Setting)
        { 
            _ISetting= Setting;
        
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<SettingDTOs>>> Get()
        {
            try 
            {

                var Data = _ISetting.GetAll();

                return Ok(ApiResponse<List<SettingDTOs>>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<List<SettingDTOs>>.FailResponse
                    ("Data Access Exception ",new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<SettingDTOs>>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }


        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<SettingDTOs>> Get(Guid id)
        {
            try
            {

                var Data = _ISetting.GetById(id);

                return Ok(ApiResponse<SettingDTOs>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<SettingDTOs>.FailResponse
                    ("Data Access Exception ", new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<SettingDTOs>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }
        }

        // POST api/<ShippingTypesController>
       
    }
}
