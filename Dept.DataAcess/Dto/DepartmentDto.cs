using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public Guid DepartmentGuid { get; set; }
        public string? DepartmentName { get; set; }

        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();

    }
}
