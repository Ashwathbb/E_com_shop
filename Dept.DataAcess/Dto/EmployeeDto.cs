using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }    
        public int DepartmentId { get; set; }
        public string? FirstName { get; set; }
    }
}
