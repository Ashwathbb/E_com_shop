using Dept.DataAcess.Dto;
using Dept.Repository.Repositories;
using Microsoft.Build.Experimental.ProjectCache;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.Services.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerServices(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }


        public async Task<IEnumerable<CustomerDto>> GetAllCustomerAsync()
        {
           var customer=await _customerRepo.GetAllAsync();
            return customer;
        }

        public async Task<CustomerDto> GetByID(int id)
        {
            var cust = await _customerRepo.GetByIdAsync(id);
            return cust;
        }

        public async Task UpdateCustomer(CustomerUpdateDto customerUpdateDto)
        {
            await _customerRepo.UpdateCustomerAsync(customerUpdateDto);
        }
        public async Task AddCustomer(CustomerDto customerDto)
        {
            await _customerRepo.AddCustomerAsync(customerDto);

        }
        public async Task DeleteCustomer(int id)
        {
           await _customerRepo.DeleteCustomerAsync(id);
        }

    }
}
