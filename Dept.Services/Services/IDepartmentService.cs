using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using System.Threading.Tasks;
namespace Dept.Services.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByidAsync(int id);
        Task<DepartmentDto> GetDepartmentByGuidAsync(Guid departmentGuid);
        Task AddDepartmentAsync(DepartmentDto departmentdto);
        Task<UpdatedeptDto> UpdateDepartmentAsync(UpdatedeptDto departmentDto);
        Task DeleteDepartmentAsync(int id);
        Task AddRolesAsync(int departmentId, IEnumerable<RoleDto> roles);
        Task<IEnumerable<AllTablesDto>> Get_Seven_Table_Data();
       // Task InsertComprehensiveDataAsync(CustomerDto customerDto, DepartmentDto departmentDto);

        Task InsertCustomerAsync(CustomerDto customerDto);
        Task InsertDepartmentAsync(DepartmentDto departmentdto);
    }
}
