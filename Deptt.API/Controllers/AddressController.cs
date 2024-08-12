using Dept.DataAcess.Dto;
using Dept.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;

namespace Deptt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddresServices _addresServices;
       public AddressController(IAddresServices addresServices)
        {
            _addresServices = addresServices;
        }
        [HttpGet]
        public async Task<IActionResult> Get_allAddressDEtail()
        {
            var address = await _addresServices.GetAllAddressDetail();
            return Ok(address);
        }
        [HttpGet("get_by_id")]
        public async Task<ActionResult> GetByID(int id)
        {
            var address=await _addresServices.GetByID(id);  
            return Ok(address); 
        }
        [HttpPost]
        public async Task<ActionResult> Add_AddrssDetail([FromBody] AddressDto addressDto)
        {
            await _addresServices.AddAddress(addressDto);
            return Ok();
        }
        [HttpPost]
        [Route("Insert_4_tables")]
        public async Task<ActionResult> Add_AddrssDetail([FromBody] Cust_Add_OrderDto cust_Add_OrderDto)
        {
            await _addresServices.AddCust_Add_OrderAsync(cust_Add_OrderDto);
            return Ok();
        }
        [HttpPost]
        [Route("Insert_into_7tables")]
        public async Task<ActionResult> Insert_Dept_Cust([FromBody] Dept_CustDto dept_CustDto)
        {
            await _addresServices.Insert_Dept_cust(dept_CustDto);
            return Ok("suscefull inserted dta to cust and dept");    
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update_address(int id,AddressUpdateDto addressUpdateDto)
        {
            try 
            {
                if(id!= addressUpdateDto.AddressID)
                {
                    return BadRequest("Address missed ..");
                }
                await _addresServices.UpdateAddress(addressUpdateDto);
                return Ok("address updated successfully.");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAllData([FromBody] Cust_Add_OrderDto custAddOrderDto)
        {
            if (custAddOrderDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                await _addresServices.Update_Dept_cust(custAddOrderDto);
                return Ok("Data updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
