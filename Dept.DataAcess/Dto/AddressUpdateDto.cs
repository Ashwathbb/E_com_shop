using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class AddressUpdateDto
    {
        public int AddressID {  get; set; } 
        public string? Street { get; set; }  
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
