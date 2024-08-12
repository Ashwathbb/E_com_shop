using Dept.DataAcess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.Services.Services
{
    public interface IAddresServices
    {
        Task<IEnumerable<AddressDto>> GetAllAddressDetail();
        Task<AddressDto> GetByID(int id);
        Task AddAddress(AddressDto addressDto);
        Task UpdateAddress(AddressUpdateDto addressUpdateDto);

        Task DeleteAddress(int id);

        Task AddCust_Add_OrderAsync(Cust_Add_OrderDto custAddOrderDto);
        Task Insert_Dept_cust(Dept_CustDto dept_CustDto);
        Task Update_Dept_cust(Cust_Add_OrderDto cust_Add_OrderDto );
    }
}
