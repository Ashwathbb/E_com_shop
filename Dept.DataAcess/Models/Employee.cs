using System;
using System.Collections.Generic;

namespace Dept.DataAcess.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? DepartmentId { get; set; }

    public Guid? EmployeeGuid { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
