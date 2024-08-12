using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class SimpleCustomerDto
    {
        public Guid DepartmentGuid {  get; set; }
        public int CustomerID {  get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
