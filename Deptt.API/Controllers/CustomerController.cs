using Dept.DataAcess.Dto;
using Dept.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Deptt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _services;

        public CustomerController(ICustomerServices customerServices)
        {
            _services = customerServices;
        }

      
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var cust= await _services.GetAllCustomerAsync();
            return Ok(cust);
        }
        [HttpGet("Get_by_customerID")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var cust = await _services.GetByID(id);
            return Ok(cust);
        }
        [HttpPost]
        public async Task<ActionResult> Add_Customers([FromBody] CustomerDto customerDto)
        {
            await _services.AddCustomer(customerDto);
            return Ok();
        }
        [HttpPut("customer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customerUpdateDto)
        {
            try
            {
                if (id != customerUpdateDto.CustomerID)
                {
                    return BadRequest("Customer ID mismatch.");
                }

                await _services.UpdateCustomer(customerUpdateDto);
                return Ok("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                await _services.DeleteCustomer(id);
                return Ok("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
