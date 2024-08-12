using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
