using AutoMapper;
using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using Dept.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.Services.Services
{
    public class AddresServices : IAddresServices
    {
        private readonly IAddressRepo _addressRepo;
       // private readonly IMapper _mapper;
        public AddresServices(IAddressRepo addressRepo)
        {

            _addressRepo = addressRepo;
          //  _mapper = mapper;
        }
        public async Task AddAddress(AddressDto addressDto)
        {
            await _addressRepo.AddAddressdetailAsync(addressDto);

        }

        public Task DeleteAddress(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AddressDto>> GetAllAddressDetail()
        {
          var address= await _addressRepo.GetAddressDetailAsync();
            return address;
        }

        public async Task<AddressDto> GetByID(int id)
        {
            var address = await _addressRepo.GetAddressByIdAsync(id);
            return address;
        }

        public async Task UpdateAddress(AddressUpdateDto addressUpdateDto)
        {
          await _addressRepo.UpdateAddressdetailAsync(addressUpdateDto);    
        }
        //public async Task ADD_ALL_TablesData(Cust_Add_OrderDto custAddOrderDto)
        //{
        //    //var customer = _mapper.Map<SimpleCustomerDto, Customer>(cust_Add_OrderDto.Customer);
        //    //var addresses = _mapper.Map<List<AddressDto>, List<Address>>(cust_Add_OrderDto.Addresses);
        //    //var orders = _mapper.Map<List<OrderDto>, List<Order>>(cust_Add_OrderDto.Orders);
        //    //var sales = _mapper.Map<List<SalesDto>, List<Sale>>(cust_Add_OrderDto.Sales);

        //    //foreach (var address in addresses)
        //    //{
        //    //    address.CustomerId = customer.CustomerID; // Assuming CustomerId is set up correctly in Address entity
        //    //    await _addressRepo.AddAddressAsync(address);
        //    //}

        //    //foreach (var order in orders)
        //    //{
        //    //    order.CustomerId = customer.CustomerID; // Assuming CustomerId is set up correctly in Order entity
        //    //    await _addressRepo.AddOrderAsync(order);
        //    //}

        //    //foreach (var sale in sales)
        //    //{
        //    //    await _addressRepo.AddSalesAsync(sale);
        //    //}
            
        //}

        public async Task AddCust_Add_OrderAsync(Cust_Add_OrderDto custAddOrderDto)
        {
            await _addressRepo.ADD_ALL_TablesData(custAddOrderDto);
        }

        public async Task Insert_Dept_cust(Dept_CustDto dept_CustDto)
        {
          await _addressRepo.Insert_Dept_cust(dept_CustDto);
        }

        public async Task Update_Dept_cust(Cust_Add_OrderDto cust_Add_OrderDto)
        {
            await _addressRepo.UpdateAllTablesData(cust_Add_OrderDto);
        }
    }
}
