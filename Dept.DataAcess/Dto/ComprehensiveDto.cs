using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class ComprehensiveDto
    {
        // Customer Information
        public CustomerDto? Customer { get; set; }
        public DepartmentDto? Department { get; set; }
        //public List<AddressDto>? Addresses { get; set; }
        //public List<OrderDto>? Orders { get; set; }
        //public List<SalesDto>? Sales { get; set; }



        //// Employee Information
        //public List<EmployeeDto>? Employees { get; set; }

        // Department Information


        // Role Information
        // public List<RoleDto>? Roles { get; set; }
    }
}
