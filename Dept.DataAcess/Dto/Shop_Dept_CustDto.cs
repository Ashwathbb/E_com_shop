using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Shop.DataAccess.DTOs

namespace Dept.DataAcess.Dto
{
    public class Shop_Dept_CustDto
    {
        /*
         * this is nmot a good way for creating other dtos here becuse there is no realtion 
        //public UserDto User { get; set; }

        **/
        public int UserId { get; set; }
        public Guid UsersInfoGuid { get; set; }
        public string? UserName { get; set; }
        public Guid DepartmentGuid { get; set; }
        public Dept_CustDto? dept_CustDto { get; set; }
      

    }
}
