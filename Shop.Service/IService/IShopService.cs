//using Department.DataAcess.Dto;
using Dept.DataAcess.Dto;
using Shop.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service.IService
{
    public interface IShopService
    {
        Task<IEnumerable<UsersInfoDepartment>> GetUsersWithDepartmentsAsync();
        Task AddUserAndDepartmentAsync(UsersInfoDepartment usersInfoDto);
        Task Insert_cust_shop(UserDto userDto,Dept_CustDto dept_CustDto);
        Task cust_shop(Cust_Add_OrderDto cust_Add_OrderDto, UsersInfoDepartment usersInfoDto);
        Task CreateUser(CreateUserDto userDto);
        Task<IEnumerable<AllTablesDto>> Get_all_Tables();
        Task<IEnumerable<AllTablesDto>> Get_7_tables();
        //Task<Shop_Dept_CustDto_shop> GetShopDeptCustByIdAsync(int id);
        // Task AddUserAndDepartmentAsync(UsersInfoDepartment usersInfoDepartment);
        //Task<IEnumerable<UsersInfoDepartment>> GetUsersWithDepartmentsAsync(Guid usersInofGuid);
        // Task<UsersInfoDepartment> GetUserWithDepartmentAsync(Guid usersInfoGuid);
    }
}
 