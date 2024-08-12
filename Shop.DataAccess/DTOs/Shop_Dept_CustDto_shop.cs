using Dept.DataAcess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public  class Shop_Dept_CustDto_shop
    {
        public UserDto? User { get; set; }
        public Dept_CustDto? DeptCust { get; set; }
    }
}
