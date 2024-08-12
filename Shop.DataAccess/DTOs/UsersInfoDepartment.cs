using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public class UsersInfoDepartment
    {
        public int UserId { get; set; }
        public Guid UsersInfoGuid { get; set; }
        public string? UserName { get; set; }
        public Guid DepartmentGuid { get; set; }
        public string? DepartmentName { get; set; }

    }
}
