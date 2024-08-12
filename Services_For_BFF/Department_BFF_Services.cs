using Shop_BFF.DTOs.DeptDtos;
using System.Text;
using System.Text.Json;



namespace Services_For_BFF
{

    public class Department_BFF_Services : IDepartment_BFF_Services
    {
        private readonly HttpClient _httpClient;
        public Department_BFF_Services(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DepartmentApiClient");
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var response = await _httpClient.GetAsync("/api/Department");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var departments = JsonSerializer.Deserialize<List<DepartmentDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return departments;
        }

        public async Task<DepartmentDto> GetDeptByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Department/GetDeptById?id={id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var departmentDto = JsonSerializer.Deserialize<DepartmentDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return departmentDto;
        }
        public async Task<DepartmentDto> GetDepartmentByGuidAsync(Guid guid)
        {
            var response = await _httpClient.GetAsync($"/api/Department/GetByGuid?departmentguid={guid}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var departmentDto = JsonSerializer.Deserialize<DepartmentDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return departmentDto;
        }
       
        public async Task AddDepartmentAsync(DepartmentDto departmentDto)
        {
            var json = JsonSerializer.Serialize(departmentDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Department", content);
            response.EnsureSuccessStatusCode();
            JsonSerializer.Deserialize<DepartmentDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<UpdatedeptDto> UpdateDepartmentAsync(UpdatedeptDto departmentDto)
        {
            var json = JsonSerializer.Serialize(departmentDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Department/{departmentDto.DepartmentId}", content);
            response.EnsureSuccessStatusCode();

            var updatedDept = await response.Content.ReadAsStringAsync();
            var updatedDepartmentDto = JsonSerializer.Deserialize<UpdatedeptDto>(updatedDept, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return updatedDepartmentDto;
        }
    }
}
