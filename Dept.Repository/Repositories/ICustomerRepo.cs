using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.Repository.Repositories
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();    
        Task<CustomerDto> GetByIdAsync(int id);
        Task AddCustomerAsync(CustomerDto customerDto);
        Task UpdateCustomerAsync(CustomerUpdateDto customerUpdateDto);
        Task DeleteCustomerAsync( int id);
       // Task ADD_Cust_Address_Order(Cust_Add_OrderDto cust_Add_OrderDto);
        // Task AddCustomerAsync(Customer customer);
        //Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
