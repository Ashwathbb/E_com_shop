using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class RoleDto
    {
    
        public int RoleId { get; set; } 
        public Guid RoleGuid { get; set; }
        public int DepartmentId { get; set; }
        public string? RoleName { get; set; }
    }
}
