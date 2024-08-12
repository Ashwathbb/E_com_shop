using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public class UserProductSelectionDto
    {
        public int UserId { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
