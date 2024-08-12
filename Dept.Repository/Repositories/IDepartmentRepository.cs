using Dept.DataAcess.Dto;
using Microsoft.EntityFrameworkCore.Storage;

namespace Dept.Repository.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByidAsync(int id);

        Task<DepartmentDto> GetDepartmentByGuidAsync(Guid departmentGuid);
        Task AddDepartmentAsync(DepartmentDto departmentdto);
        Task<UpdatedeptDto> UpdateDepartmentAsync(UpdatedeptDto department);
        Task DeleteDepartmentAsync(int id);
        Task AddRolesAsync(int departmentId, IEnumerable<RoleDto> roles);
        Task<IEnumerable<AllTablesDto>> Get_Seven_Table_Fileds();
        //insert data to ALL TABLES OF ONE CALL OF API
        Task InsertCustomerAsync(CustomerDto customerDto);
        Task InsertDepartmentAsync(DepartmentDto departmentDto);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
