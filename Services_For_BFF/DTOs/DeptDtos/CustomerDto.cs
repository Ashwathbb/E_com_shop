using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_For_BFF.DTOs.DeptDtos
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public Guid DepartmentGuid { get; set; }
        public string? FirstName { get; set; }
        public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
        public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }
}
