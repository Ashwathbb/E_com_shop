//using Department.DataAcess.Dto;
using Dept.DataAcess.Dto;
using Shop.DataAccess.DTOs;

namespace Shop.Service.IService
{
    public interface IDepartmentServiceClient
    {
        Task<DepartmentDto> GetDepartmentByGuidAsync(Guid departmentGuid);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<IEnumerable<AllTablesDto>> GetSevenTables();

        Task<DepartmentDto> AddDepartmentAsync(DepartmentDto departmentDto);
        Task<SimpleCustomerDto> Add_Customer(SimpleCustomerDto customerDto);
        Task<IEnumerable<UserDepartmentDto>> GetUsersWithDepartmentsAsync();
        // for creation
        Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto);
        Task Insert_customer_with_Department(Dept_CustDto dept_CustDto);
        Task<Cust_Add_OrderDto> Create_cust_address_order(Cust_Add_OrderDto cust_Add_OrderDto);
        Task<IEnumerable<AllTablesDto>> Get_all_tables();


    }
}
