using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_For_BFF.DTOs.DeptDtos
{
    public class AddressDto
    {
        public int AddressID { get; set; }
        public int CustomerID { get; set; }
        public string? ZipCode { get; set; }
    }
}
