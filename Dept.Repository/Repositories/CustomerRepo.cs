using Dapper;
using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using System.Data;

namespace Dept.Repository.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly IDbConnection _dbconnection;
        private readonly DepartmentDbContext _dbContext;

        public CustomerRepo(IDbConnection dbconnection, DepartmentDbContext dbContext)
        {
            _dbconnection = dbconnection;
            _dbContext = dbContext;
        }

       
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            // Implement Dapper method to fetch all customers
            var sql = "select * from Customer";
            var customers= await _dbconnection.QueryAsync<CustomerDto>(sql);
            foreach(var customer in customers)
            {
                var addressSql = "select * from Address where CustomerID=@CustomerID";
                var ordersSql = "select * from Orders where CustomerID=@CustomerID";
                var address= await _dbconnection.QueryAsync<AddressDto>(addressSql, new { CustomerID=customer.CustomerID});
                var orders=await _dbconnection.QueryAsync<OrderDto>(ordersSql, new { CustomerID=customer.CustomerID});
                customer.Addresses=address.ToList();
                customer.Orders=orders.ToList();
            }
            return customers.ToList();
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            // Implement Dapper method to fetch all customers
            var sql = "select * from Customer where  CustomerID=@CustomerID";
            var cust= await _dbconnection.QuerySingleOrDefaultAsync<CustomerDto>(sql, new { CustomerID = id });

            if (cust != null)
            {
                var addressSql = "select * from Address where CustomerID=@CustomerID";
                var ordersSql = "select * from Orders where CustomerID=@CustomerID";

                var address = await _dbconnection.QueryAsync<AddressDto>(addressSql, new { CustomerID = cust.CustomerID });
                var  orders  =await _dbconnection.QueryAsync<OrderDto>(ordersSql, new { CustomerID = cust.CustomerID });
                cust.Addresses=address.ToList();
                cust.Orders = orders.ToList();
            }
            return cust;
        }

       
        // insert the data using Entity framework 
        public async Task AddCustomerAsync(CustomerDto customerDto)
        {
            // here we dont use Dapper fro create insted of dapper use EF
            // Implement Entity Framework method to add a customer
            var cust = new Customer
            {
                CustomerId= customerDto.CustomerID, 
                DepartmentGuid= customerDto.DepartmentGuid,
                FirstName = customerDto.FirstName,
                Addresses = customerDto.Addresses.Select(x => new Address
                                                            {
                                                                AddressId=x.AddressID,
                                                                CustomerId = x.CustomerID,
                                                                ZipCode = x.ZipCode
                                                            }
                                                       ).ToList(),
                Orders = customerDto.Orders.Select(y => new Order
                                                        {
                                                            OrderId=y.OrderID,  
                                                            CustomerId = y.CustomerID,
                                                            OrderDate = y.OrderDate,
                                                        }
                                                    ).ToList()

            };
            await _dbContext.Customers.AddAsync(cust);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(CustomerUpdateDto customerUpdateDto)
        {
            // Implement Entity Framework method to update a customer
            var customer = await _dbContext.Customers.FindAsync(customerUpdateDto.CustomerID);


            if (customer != null)
            {

               // customer.CustomerId = customerUpdateDto.CustomerID;
                customer.LastName = customerUpdateDto.LastName;
                customer.Email = customerUpdateDto.Email;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Customer not found.");
            }
        }
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Customer not found.");
            }
        }

      
    }
}
