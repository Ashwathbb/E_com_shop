using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using Dept.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deptt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }
        [HttpGet("GetDeptById")]
        public async Task<IActionResult> GetDeptById(int id)
        {
            var deptId = await _departmentService.GetDepartmentByidAsync(id);
            if (deptId == null)
            {
                return NotFound();
            }
            return Ok(deptId);
        }
        [HttpGet("GetByGuid")]
        public async Task<IActionResult> GetDeptByGuid(Guid departmentguid)
        {
            var deptId = await _departmentService.GetDepartmentByGuidAsync(departmentguid);
            if (deptId == null)
            {
                return NotFound();
            }
            return Ok(deptId);
        }
        [HttpPost]
        public async Task<ActionResult> AddDepartment([FromBody] DepartmentDto departmentdto)
        {
            await _departmentService.AddDepartmentAsync(departmentdto);
            //with the help of services we are post the data
            return CreatedAtAction(nameof(GetDeptById), new { id = departmentdto.DepartmentId }, departmentdto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdatedeptDto>> UpdateDepartment(int id, [FromBody] UpdatedeptDto departmentDto)
        {
            if (id != departmentDto.DepartmentId)
            {
                return BadRequest();
            }
            return await _departmentService.UpdateDepartmentAsync(departmentDto);
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }

        [HttpPost("{departmentId}/roles")]
        public async Task<ActionResult> AddRoles(int departmentId, [FromBody] IEnumerable<RoleDto> roles)
        {
            await _departmentService.AddRolesAsync(departmentId, roles);
            return NoContent();
        }
        [HttpGet("Get_seven_tables")]
        public async Task<ActionResult<IEnumerable<AllTablesDto>>> GetDepartments()
        {
            var departments = await _departmentService.Get_Seven_Table_Data();
            return Ok(departments);
        }

        // POST api/department/department
        [HttpPost("department")]
        public async Task<IActionResult> InsertDepartment([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                await _departmentService.InsertDepartmentAsync(departmentDto);

                return Ok("Department inserted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
