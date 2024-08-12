using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class AddressDto
    {
        public int AddressID { get; set; } 
        public int CustomerID { get; set; }
        public string? ZipCode { get; set; }
    }
}
