//using Department.DataAcess.Dto;
using Azure.Core;
using Dept.DataAcess.Dto;
using Shop.DataAccess.DTOs;
using Shop.Repository.IRepositories;
using System;

namespace Shop.Service.IService.Services
{
    public class ShopService : IShopService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentServiceClient _departmentServiceClient;
        public ShopService(IUserRepository userRepository, IDepartmentServiceClient departmentServiceClient)
        {
            _userRepository = userRepository;
            _departmentServiceClient = departmentServiceClient;
        }
        /****************************************
        *to get the data from both databse with the help 
        *get all user--->using GellAllUserasync() method
        *get all department ->using byid() method 
        *then join both 
        ***************************************/
        public async Task<IEnumerable<UsersInfoDepartment>> GetUsersWithDepartmentsAsync()
        {
            // Fetch users from the database
            var users = await _userRepository.GetAllUsersAsync();

            // Filter out invalid or empty department GUIDs // Filtering Valid Department GUIDs
            var validDepartmentGuids = users
                .Where(u => u.DepartmentGuid != Guid.Empty)//filer filters out invalid guid
                .Select(u => u.DepartmentGuid)//projection
                .Distinct()//Ensures only unique department GUIDs are selected.
                .ToArray();//Converts the result to an array.

            var departments = new List<DepartmentDto>();

            // Fetch each department details using the DepartmentServiceClient
            foreach (var guid in validDepartmentGuids)
            {
                try
                {
                    var department = await _departmentServiceClient.GetDepartmentByGuidAsync(guid);
                    if (department != null)
                    {
                        departments.Add(department);
                    }
                }
                catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Handle 404 - Not Found
                    // Log the exception and details
                    Console.WriteLine($"Error fetching department with GUID {guid}: {ex.Message}");
                }
            }

            // Join users with departments
            var userDepartmentData = from user in users
                                     join dept in departments on user.DepartmentGuid equals dept.DepartmentGuid into userDepts
                                     from userDept in userDepts.DefaultIfEmpty()
                                     select new UsersInfoDepartment
                                     {
                                         UserId = user.UserId,
                                         UsersInfoGuid = user.UsersInfoGuid,
                                         UserName = user.UserName,
                                         DepartmentGuid = user.DepartmentGuid,
                                         DepartmentName = user.DepartmentGuid == Guid.Empty ? null : userDept?.DepartmentName ?? "Department Not Found"
                                     };

            return userDepartmentData;
        }


        public async Task AddUserAndDepartmentAsync(UsersInfoDepartment usersInfoDto)
        {

            var userGuid = Guid.NewGuid();

            var user_dept = new UsersInfoDepartment
            {
                // UserId = usersInfoDto.UserId,
                UsersInfoGuid = userGuid,
                UserName = usersInfoDto.UserName,
                DepartmentGuid = userGuid
            };

            var departmentDto = new DepartmentDto
            {
                DepartmentName = usersInfoDto.DepartmentName,
                DepartmentGuid = userGuid
            };


            await _departmentServiceClient.AddDepartmentAsync(departmentDto);
            await _userRepository.AddUsersInfoAsync(user_dept);
            //  await _departmentServiceClient.Add_Customer(customerDto);
        }


        public async Task Insert_cust_shop(UserDto userDto, Dept_CustDto dept_CustDto)
        {
            var userGuid = Guid.NewGuid();
            var users = new UsersInfoDepartment
            {
                UsersInfoGuid = userGuid,
                UserName = userDto.UserName,
                DepartmentGuid = userGuid
            };

            await _departmentServiceClient.Insert_customer_with_Department(dept_CustDto);
            await _userRepository.AddUsersInfoAsync(users);
        }
        public async Task cust_shop(Cust_Add_OrderDto cust_Add_OrderDto, UsersInfoDepartment usersInfoDto)
        {
            var userGuid = Guid.NewGuid();

            usersInfoDto.UsersInfoGuid = userGuid;
            //  departmentDto.DepartmentGuid = userGuid;
            await _departmentServiceClient.Create_cust_address_order(cust_Add_OrderDto);
            await _userRepository.AddUsersInfoAsync(usersInfoDto);

        }
        public async Task CreateUser(CreateUserDto userDto)
        {
            DepartmentDto deptResult = await _departmentServiceClient.GetDepartmentByGuidAsync(userDto.DepartmentGuid);
            if (deptResult.DepartmentGuid == null)
            {
                userDto.DepartmentGuid = Guid.Empty;
            }

            await _userRepository.AddUser(userDto);
        }

        public async Task<IEnumerable<AllTablesDto>> Get_all_Tables()
        {
           var cust= await _departmentServiceClient.Get_all_tables();
            return cust;
        }
        public async Task<IEnumerable<AllTablesDto>> Get_7_tables()
        {
            var cust = await _departmentServiceClient.GetSevenTables();
            return cust;
        }
        

    }
}
