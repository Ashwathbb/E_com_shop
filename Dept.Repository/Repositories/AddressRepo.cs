using AutoMapper;
using Dapper;
using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Nest;
using System.Data;

namespace Dept.Repository.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private readonly IDbConnection _connection;
        private readonly DepartmentDbContext _dbContext;

        public AddressRepo(IDbConnection dbConnection,DepartmentDbContext departmentDbContext)
        {
            _connection = dbConnection;
            _dbContext = departmentDbContext;
         //   _mapper = mapper;
        }
        public async Task AddAddressdetailAsync(AddressDto addressDto)
        {
            var add = new Address
            {
                AddressId=addressDto.AddressID,
                CustomerId=addressDto.CustomerID,   
                ZipCode=addressDto.ZipCode
            };
            await _dbContext.Addresses.AddAsync(add);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAddressDetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AddressDto> GetAddressByIdAsync(int id)
        {
            var sql = " select * from Address where AddressID=@AddressID";
            var address= await _connection.QuerySingleOrDefaultAsync<AddressDto>(sql,new { AddressID = id });
            return address;
        }

        public async Task<IEnumerable<AddressDto>> GetAddressDetailAsync()
        {
            var sql = "Select * from Address";
            var address = await _connection.QueryAsync<AddressDto>(sql);    
            return address.ToList();
        }

        public async Task UpdateAddressdetailAsync(AddressUpdateDto addressUpdateDto)
        {
           var add = await _dbContext.Addresses.FindAsync(addressUpdateDto.AddressID);
            if (add != null)
            {  
                add.AddressId= addressUpdateDto.AddressID;  
                add.Street= addressUpdateDto.Street;
                add.City= addressUpdateDto.City;
                add.State = addressUpdateDto.State;
                await _dbContext.SaveChangesAsync();    
            }
            else
            {
                     throw new ArgumentException("Address not Foumd."); 
            }
        }

        public async Task ADD_ALL_TablesData(Cust_Add_OrderDto custAddOrderDto)
        {
            // Map and add Customer
            var customer = new Customer
            {
                DepartmentGuid = custAddOrderDto.Customer.DepartmentGuid,
                CustomerId=custAddOrderDto.Customer.CustomerID,
                FirstName = custAddOrderDto.Customer.FirstName,
                Email = custAddOrderDto.Customer.Email
              
            };
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync(); // Save to generate customer ID

            // Map and add Addresses
            if (custAddOrderDto.Addresses != null && custAddOrderDto.Addresses.Any())
            {
                var addresses = custAddOrderDto.Addresses.Select(a => new Address
                {
                    AddressId= a.AddressID, 
                    ZipCode = a.ZipCode,
                    CustomerId = a.CustomerID // Foreign key
                }).ToList();

                _dbContext.Addresses.AddRange(addresses);
                await _dbContext.SaveChangesAsync(); // Save to generate customer ID
            }
            if(custAddOrderDto.Orders != null && custAddOrderDto.Orders.Any())
            {
                var orders =custAddOrderDto.Orders.Select(o=> new Order
                {
                    OrderId= o.OrderID,
                    OrderDate=o.OrderDate,
                    CustomerId=o.CustomerID
                }
                );
                _dbContext.Orders.AddRange(orders);
                await _dbContext.SaveChangesAsync(); // Save to generate customer ID
            }
            // Map and add Sales
            if (custAddOrderDto.Sales != null && custAddOrderDto.Sales.Any())
            {
                var sales = custAddOrderDto.Sales.Select(s => new Sale
                {
                    SaleId= s.SalesID,
                    Amount = s.Amount,
                    OrderId = s.OrderID // Foreign key
                }).ToList();

                _dbContext.Sales.AddRange(sales);
                await _dbContext.SaveChangesAsync(); // Save to generate customer ID
            }
        }

        public async Task Insert_Dept_cust(Dept_CustDto dept_CustDto)
        {
            //map and add the departments 
            var department = new Department
            {
                DepartmentName = dept_CustDto.Departments.DepartmentName
            };
            _dbContext.Departments.AddRange(department);
            await _dbContext.SaveChangesAsync();
            if (dept_CustDto.Employee != null && dept_CustDto.Employee.Any())
            {
                var employee = dept_CustDto.Employee.Select(e => new Employee
                {
                    EmployeeId = e.EmployeeID,
                    DepartmentId=e.DepartmentId,
                    FirstName = e.FirstName // Foreign key

                }).ToList();
                _dbContext.Employees.AddRange(employee);
                await _dbContext.SaveChangesAsync();
            };
            if (dept_CustDto.Customer != null && dept_CustDto.Customer.Any())
            {
                var customer = dept_CustDto.Customer.Select(c=>new Customer
                {
                    DepartmentGuid = c.DepartmentGuid,
                    CustomerId = c.CustomerID,
                    FirstName = c.FirstName,
                    Email = c.Email
                }).ToList();
                _dbContext.Customers.AddRange(customer);
                await _dbContext.SaveChangesAsync();
            }
            if (dept_CustDto.Addresses != null && dept_CustDto.Addresses.Any()) 
            {
                var address = dept_CustDto.Addresses.Select(a => new Address
                {
                    AddressId = a.AddressID,
                    ZipCode = a.ZipCode,
                    CustomerId = a.CustomerID // Foreign key

                }).ToList();
                _dbContext.Addresses.AddRange(address); 
                await _dbContext.SaveChangesAsync();
            };
            if(dept_CustDto.Orders!=null && dept_CustDto.Orders.Any())
            {
                var Orders = dept_CustDto.Orders.Select(o => new Order
                {
                    OrderId = o.OrderID,
                    OrderDate = o.OrderDate,
                    CustomerId = o.CustomerID
                }).ToList();
                _dbContext.Orders.AddRange(Orders); 
                await _dbContext.SaveChangesAsync();
            }
            if(dept_CustDto.Sales!=null && dept_CustDto.Sales.Any())
            {
                var sales = dept_CustDto.Sales.Select(s => new Sale
                {
                    SaleId = s.SalesID,
                    Amount = s.Amount,
                    OrderId = s.OrderID // Foreign key

                }).ToList();
                _dbContext.Sales.AddRange(sales);   
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task UpdateAllTablesData(Cust_Add_OrderDto custAddOrderDto)
        {

            // Update Customer
            var customer = await _dbContext.Customers.FindAsync(custAddOrderDto.Customer.CustomerID);
            if (customer != null)
            {
                customer.DepartmentGuid = custAddOrderDto.Customer.DepartmentGuid;
                customer.FirstName = custAddOrderDto.Customer.FirstName;
                customer.Email = custAddOrderDto.Customer.Email;

                _dbContext.Customers.Update(customer);
            }
            // Update Addresses
            if (custAddOrderDto.Addresses != null && custAddOrderDto.Addresses.Any())
            {
                foreach (var addressDto in custAddOrderDto.Addresses)
                {
                    var address = await _dbContext.Addresses.FindAsync(addressDto.AddressID);
                    if (address != null)
                    {
                        address.ZipCode = addressDto.ZipCode;
                        address.CustomerId = customer.CustomerId; // Ensure foreign key is correct

                        _dbContext.Addresses.Update(address);
                    }
                }
            }
            // Save all changes to the database
            await _dbContext.SaveChangesAsync();
        }
    }
}
