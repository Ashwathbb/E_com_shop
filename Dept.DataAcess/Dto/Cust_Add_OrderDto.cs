using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class Cust_Add_OrderDto
    {
        public SimpleCustomerDto? Customer { get; set; }
        public List<AddressDto>? Addresses { get; set; }
        public List<OrderDto>? Orders { get; set; }
        public List<SalesDto>? Sales { get; set; } = new List<SalesDto>();  


    }
}
