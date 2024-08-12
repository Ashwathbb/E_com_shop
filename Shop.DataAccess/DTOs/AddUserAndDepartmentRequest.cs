using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.DTOs
{
    public class AddUserAndDepartmentRequest
    {
        public Guid UsersInfoGuid { get; set; }

        public Guid DepartmentGuid { get; set; }
        public int CustomerID {  get; set; }    
        public string UserName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
    }
}
