using Dept.Services.Services;
using Dept.DataAcess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Department.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService) {
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
        public async Task<IActionResult> GetDeptByGuid(Guid  departmentguid)
        {
            var deptId = await _departmentService.GetDepartmentByGuidAsync(departmentguid);
            if (deptId == null)
            {
                return NotFound();
            }
            return Ok(deptId);
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            var dept = await _departmentService.AddDepartmentAsync(department);
            return dept;
        }
    }
}
