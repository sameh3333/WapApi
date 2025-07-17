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
    public class CityController : ControllerBase
    {

        ICity _City;

        public CityController(ICity city)
        { 
            _City=city;
        
        }

        // GET: api/<ShippingTypesController>
        [HttpGet]
        public ActionResult<ApiResponse<List<CityDTOs>>> Get()
        {
            try 
            {

                var Data = _City.GetAll();

                return Ok(ApiResponse<List<CityDTOs>>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<List<CityDTOs>>.FailResponse
                    ("Data Access Exception ",new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<CityDTOs>>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }


        }

        // GET api/<ShippingTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<CityDTOs>> Get(Guid id)
        {
            try
            {

                var Data = _City.GetById(id);

                return Ok(ApiResponse<CityDTOs>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<CityDTOs>.FailResponse
                    ("Data Access Exception ", new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CityDTOs>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }
        }

         [HttpGet("GetByCountry/{id}")]
        //[HttpGet("GetByCountry/{CountryId}")]
        public ActionResult<ApiResponse<CityDTOs>> GetByCountry(Guid id)
        {
            try
            {

                var Data = _City.GetByIdCounty(id);

                return Ok(ApiResponse<List<CityDTOs>>.SuccessResponse(Data));

            }
            catch (DataAccessException daex)
            {
                return StatusCode(500, ApiResponse<CityDTOs>.FailResponse
                    ("Data Access Exception ", new List<string>() { daex.Message }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CityDTOs>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }
        }

    }
}
