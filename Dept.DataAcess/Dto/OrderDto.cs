using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class OrderDto
    {
        public int OrderID  { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
