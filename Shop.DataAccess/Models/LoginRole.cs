using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Models;

public partial class LoginRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
