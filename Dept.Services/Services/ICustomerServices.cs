using Dept.DataAcess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.Services.Services
{
    public interface ICustomerServices
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomerAsync();
        Task<CustomerDto> GetByID(int id);
        Task AddCustomer(CustomerDto customerDto);
        Task UpdateCustomer(CustomerUpdateDto customerUpdateDto);

        Task DeleteCustomer(int id);
    }
}
