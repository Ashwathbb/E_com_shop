using Microsoft.AspNetCore.Mvc;

namespace Department_BFF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public DepartmentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // Method to get department by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeptById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Department/GetDeptById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var department = await response.Content.ReadAsStringAsync();
                return Ok(department);
            }

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
        // Method to get all departments
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var response = await _httpClient.GetAsync("/api/Department/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var departments = await response.Content.ReadAsStringAsync();
                return Ok(departments);
            }

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
