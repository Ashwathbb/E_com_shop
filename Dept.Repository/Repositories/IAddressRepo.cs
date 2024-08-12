using Dept.DataAcess.Dto;
using Dept.DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.Repository.Repositories
{
    public interface IAddressRepo
    {
        Task<IEnumerable<AddressDto>> GetAddressDetailAsync();
        Task<AddressDto> GetAddressByIdAsync(int id);
        Task AddAddressdetailAsync(AddressDto addressDto);
        Task UpdateAddressdetailAsync(AddressUpdateDto addressUpdateDto);
        Task DeleteAddressDetailAsync(int id);
        //------------------------------------------------------
        Task ADD_ALL_TablesData(Cust_Add_OrderDto custAddOrderDto);
        //------------------------------------------------------------
        Task Insert_Dept_cust(Dept_CustDto dept_CustDto);
        //Task AddCustomerAsync(Customer customer);
        //Task AddAddressAsync(Address address);
        //Task AddOrderAsync(Order order);
        //Task AddSalesAsync(Sale sale);
        Task UpdateAllTablesData(Cust_Add_OrderDto custAddOrderDto);
    }
}
