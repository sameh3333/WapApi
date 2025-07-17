using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WapApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WapApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {

        IShippment _shippment;
        IUserService _userService;  
        
        public ShipmentController(IShippment shippment,IUserService userservices) 
        {
        _shippment = shippment;
            _userService = userservices;
        }


        // GET: api/<ShippmentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShippmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShippmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        // POST api/<ShippmentController>
        [HttpPost("Create")]
        public async Task<IActionResult>Create([FromBody] ShippmentDTOs data)
        {
            if(data == null)
            {
                return BadRequest(ApiResponse<string>.FailResponse("Shipment data is Null"));
            }
            try
            {

                var resurlt =  _shippment.Create(data);

                return Ok(ApiResponse<object>.SuccessResponse(resurlt,"Shipment"));

            }
           
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CarrierDtos>.FailResponse
                    ("genale Exception ", new List<string>() { ex.Message }));
            }

        }

        // PUT api/<ShippmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShippmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
