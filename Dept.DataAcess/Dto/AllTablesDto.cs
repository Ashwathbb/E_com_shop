using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dept.DataAcess.Dto
{
    public class AllTablesDto
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public Guid DepartmentGuid { get; set; }
        public string? FirstName { get; set; }
        public string? ZipCode { get; set; } = null;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? RoleName { get; set; }
        public decimal Amount { get; set; }
    }
}
