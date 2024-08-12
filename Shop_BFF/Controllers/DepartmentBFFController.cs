using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Services_For_BFF;
using Shop_BFF.DTOs.DeptDtos;
using System.Net;

namespace Shop_BFF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentBFFController : ControllerBase
    {
       
        private readonly IDepartment_BFF_Services _deptService;
        private readonly IHttpContextAccessor _httpContextAccessor;
    
        public DepartmentBFFController(IDepartment_BFF_Services departmentService, IHttpContextAccessor httpContextAccessor)
        {
        
            _deptService = departmentService;
            _httpContextAccessor = httpContextAccessor;
        
        }

        [HttpGet("GetAllDepartments")]
    
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments()
        {
            var departments = await _deptService.GetAllDepartmentsAsync();
            if (departments != null)
            {
                return Ok(departments);
            }
            return StatusCode(500, "Error retrieving the departments");
        }

        [HttpGet("GetDeptById/{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDeptById(int id)
        {
            var departmentDto = await _deptService.GetDeptByIdAsync(id);
            if (departmentDto != null)
            {
                return Ok(departmentDto);
            }
            return NotFound();
        }
        [HttpGet("GetByGuid/{departmentGuid}")]
   
        public async Task<IActionResult> GetDeptByGuid(Guid departmentGuid)
        {
            var departmentDto = await _deptService.GetDepartmentByGuidAsync(departmentGuid);
            if (departmentDto != null)
            {
                return Ok(departmentDto);
            }
            return NotFound();
        }

        [HttpPost("AddDepartment")]

        public async Task<ActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
             await _deptService.AddDepartmentAsync(departmentDto);
            return CreatedAtAction(nameof(GetDeptById), new { id = departmentDto.DepartmentId }, departmentDto);
        }

        [HttpPut("UpdateDepartment")]
 
        public async Task<ActionResult<UpdatedeptDto>> UpdateDepartment([FromBody] UpdatedeptDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest("Department data is required.");
            }

            try
            {
                var updatedDepartmentDto = await _deptService.UpdateDepartmentAsync(departmentDto);

                if (updatedDepartmentDto == null)
                {
                    return NotFound("Department not found.");
                }

                return Ok(updatedDepartmentDto);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception details
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode((int)HttpStatusCode.InternalServerError, $"An unexpected error occurred: {ex.Message}");
            }
        }

        //[HttpDelete("DeleteDepartment/{id}")]
        //public async Task<ActionResult> DeleteDepartment(int id)
        //{
        //    var response = await _httpClient.DeleteAsync($"/api/Department/{id}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return NoContent();
        //    }
        //    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        //}

        //[HttpPost("AddRoles/{departmentId}")]
        //public async Task<ActionResult> AddRoles(int departmentId, [FromBody] IEnumerable<RoleDto> roles)
        //{
        //    var json = JsonSerializer.Serialize(roles);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync($"/api/Department/{departmentId}/roles", content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return NoContent();
        //    }
        //    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        //}
        //[HttpGet("GetSevenTables")]
        //public async Task<ActionResult<IEnumerable<AllTablesDto>>> GetDepartments()
        //{
        //    var response = await _httpClient.GetAsync("/api/Department/Get_seven_tables");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var departments = await response.Content.ReadAsStringAsync();
        //        return Ok(departments);
        //    }
        //    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        //}

        //[HttpPost("InsertDepartment")]
        //public async Task<IActionResult> InsertDepartment([FromBody] DepartmentDto departmentDto)
        //{
        //    var json = JsonSerializer.Serialize(departmentDto);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync("/api/Department/department", content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return Ok("Department inserted successfully.");
        //    }
        //    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        //}
    }
}
