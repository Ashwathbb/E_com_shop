using Dapper;
using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;



//using Department.DataAcess.Dto;
using System.Data;
using System.Data.Common;

namespace Dept.Repository.Repositories
{
    public class DeptRepo : IDepartmentRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly DepartmentDbContext _context;
        // private readonly ILogger<DeptRepo> _logger;
        public DeptRepo(IDbConnection dbConnection, DepartmentDbContext context)
        {
            _dbConnection = dbConnection;
            _context = context;
            // _logger = logger;   
        }
       public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {


            var sql = "SELECT * FROM Department";
            var departments = await _dbConnection.QueryAsync<DepartmentDto>(sql);
            foreach (var department in departments)
            {
                var rolesSql = "SELECT * FROM Role WHERE DepartmentId = @DepartmentId";
                var roles = await _dbConnection.QueryAsync<RoleDto>(rolesSql, new { DepartmentId = department.DepartmentId });
                department.Roles = roles.ToList();
            }
            return departments.ToList();
        }

        public async Task<DepartmentDto> GetDepartmentByidAsync(int id)
        {
            var sql = "SELECT * FROM Department WHERE DepartmentId = @Id";
            var department = await _dbConnection.QuerySingleOrDefaultAsync<DepartmentDto>(sql, new { Id = id });
            if (department != null)
            {
                var rolesSql = "SELECT * FROM Role WHERE DepartmentId = @DepartmentId";
                var roles = await _dbConnection.QueryAsync<RoleDto>(rolesSql, new { DepartmentId = department.DepartmentId });
                department.Roles = roles.ToList();
            }
            return department;

        }
        public async Task<DepartmentDto> GetDepartmentByGuidAsync(Guid departmentGuid)
        {
            var sql = "SELECT * FROM Department WHERE DepartmentGuid = @Guid";
            var department = await _dbConnection.QuerySingleOrDefaultAsync<DepartmentDto>(sql, new { Guid = departmentGuid });
            if (department != null)
            {
                //we need to display the role what and all associated with particular departmentId
                var rolesSql = "SELECT * FROM Role WHERE DepartmentId = @DepartmentId";
                var roles = await _dbConnection.QueryAsync<RoleDto>(rolesSql, new { DepartmentId = department.DepartmentId });
                department.Roles = roles.ToList();
            }
            return department;
        }
        public async Task AddDepartmentAsync(DepartmentDto departmentdto)
        {
            /****************************************
              *we need to insert the department name ad guid with help of sql query
              * then excute that query usinf Excutemethod.
             ***************************************/
            var sql = "INSERT INTO Department (DepartmentName, DepartmentGuid) VALUES (@DepartmentName, @DepartmentGuid)";
            await _dbConnection.ExecuteAsync(sql, new { departmentdto.DepartmentName, departmentdto.DepartmentGuid });
        }

        public async Task<UpdatedeptDto> UpdateDepartmentAsync(UpdatedeptDto department)
        {
            //The sqlUpdate var executes the SQL command to update the department.
            var sqlUpdate = "UPDATE Department SET DepartmentName = @DepartmentName WHERE DepartmentId = @DepartmentId";
            await _dbConnection.ExecuteAsync(sqlUpdate, new { department.DepartmentName, department.DepartmentId });

            //The second part of the method executes another SQL command to select the updated department record based on the DepartmentId.
            var sqlSelect = "SELECT * FROM Department WHERE DepartmentId = @DepartmentId";
            var updatedDepartment = await _dbConnection.QueryFirstOrDefaultAsync<UpdatedeptDto>(sqlSelect, new { department.DepartmentId });

            return updatedDepartment;
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var sql = "DELETE FROM Department WHERE DepartmentId = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task AddRolesAsync(int departmentId, IEnumerable<RoleDto> roles)
        {
            var sql = "INSERT INTO Role (RoleName, DepartmentId) VALUES (@RoleName, @DepartmentId)";
            var roleParams = roles.Select(role => new { role.RoleName, DepartmentId = departmentId });
            await _dbConnection.ExecuteAsync(sql, roleParams);
        }

        public async Task<IEnumerable<AllTablesDto>> Get_Seven_Table_Fileds()
        {
            //  throw new NotImplementedException();
            // here we are joining all the tables and fetach the data from each table
            //var data=await(from gh in _dbConnection.)
            var sql = @"select
                    d.DepartmentId,
                    d.DepartmentGuid,
                    d.DepartmentName,
                    e.EmployeeID,
                    e.FirstName,
                    r.RoleId,
                    r.RoleName,
                    c.Email as Customer_email,
                    o.OrderDate,
                    a.ZipCode,
                    s.Amount
                FROM Department d
                JOIN Employee e ON d.DepartmentId = e.DepartmentId
                join Role r ON d.DepartmentId = r.DepartmentId
                JOIN Customer c ON c.DepartmentGuid = d.DepartmentGuid
                JOIN Address a ON a.CustomerID = c.CustomerID
                JOIN Orders o ON o.CustomerID = c.CustomerID
                JOIN Sales s ON s.OrderID = o.OrderID;";

            var data = await _dbConnection.QueryAsync<AllTablesDto>(sql);
            return data;
        }
        public async Task InsertCustomerAsync(CustomerDto customerDto)
        {
            try
            {
                var customer = new Customer
                {
                    CustomerId = customerDto.CustomerID, // Ensure this is unique and set
                    DepartmentGuid = customerDto.DepartmentGuid,
                    FirstName = customerDto.FirstName,
                    Addresses = customerDto.Addresses.Select(a => new Address
                    {
                        CustomerId = a.CustomerID, // Link to the customer

                        ZipCode = a.ZipCode
                    }).ToList(),
                    Orders = customerDto.Orders.Select(o => new Order
                    {
                        CustomerId = o.CustomerID, // Link to the customer
                        OrderDate = o.OrderDate
                    }).ToList()
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateException ex)
            {
               
                throw new Exception("Error occurred while saving customer data.", ex);
            }
        }

        public async Task InsertDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                DepartmentGuid = departmentDto.DepartmentGuid,
                DepartmentName = departmentDto.DepartmentName,
                Roles = departmentDto.Roles.Select(r => new Role
                {
                    //RoleGuid = r.RoleGuid,
                    RoleName = r.RoleName
                }).ToList()
            };

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }
    }
}
