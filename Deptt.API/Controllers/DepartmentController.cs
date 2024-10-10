using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using Dept.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Deptt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly DepartmentDbContext _context;
        public DepartmentController(IDepartmentService departmentService, DepartmentDbContext context)
        {
            _departmentService = departmentService;
            _context = context;
        }
        [HttpGet("linq-operations")]
        public async Task<IActionResult> GetLinqOperations()
        {
         

            var result2 = await _context.Departments.Where(d => d.DepartmentName == "string").ToListAsync();
            var result3 = await _context.Departments.Where(d => d.DepartmentName == "string" && d.DepartmentId == 0).ToListAsync();
            var result4 = await _context.Departments.Where(d => d.DepartmentName.Contains("n")).ToListAsync();
            var result5 = await _context.Departments.Where(d => d.DepartmentName.Length == 5).ToListAsync();
            var result6 = await _context.Departments.Where(d => d.DepartmentName.StartsWith("s")).ToListAsync();
            var result7 = await _context.Departments.Where(d => d.DepartmentName == "Human Resources" && d.Roles.Any(x => x.DepartmentId == 1)).ToListAsync();
            var result8 = await _context.Departments.Where(d => d.DepartmentName == "string" && d.Roles.Any(x => x.RoleName == "string")).ToListAsync();

            var response = new
            {
             
                Result2 = result2,
                Result3 = result3,
                Result4 = result4,
                Result5 = result5,
                Result6 = result6,
                Result7 = result7,
                Result8 = result8
            };

            return Ok(response);
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
