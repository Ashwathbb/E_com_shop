

using Services_For_BFF.DTOs.DeptDtos;
using Shop_BFF.DTOs.DeptDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_For_BFF
{
    public interface IDepartment_BFF_Services
    {
        Task<DepartmentDto> GetDeptByIdAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByGuidAsync(Guid departmentGuid);
        Task AddDepartmentAsync(DepartmentDto departmentdto);
        Task<UpdatedeptDto> UpdateDepartmentAsync(UpdatedeptDto departmentDto);
        //Task DeleteDepartmentAsync(int id);
        //Task AddRolesAsync(int departmentId, IEnumerable<RoleDto> roles);
        //Task<IEnumerable<AllTablesDto>> Get_Seven_Table_Data();
        //// Task InsertComprehensiveDataAsync(CustomerDto customerDto, DepartmentDto departmentDto);

        //Task InsertCustomerAsync(CustomerDto customerDto);
        //Task InsertDepartmentAsync(DepartmentDto departmentdto);
    }
}
