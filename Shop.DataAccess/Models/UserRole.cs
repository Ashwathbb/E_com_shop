using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }

    public virtual LoginRole? Role { get; set; }

    public virtual UsersInfo? User { get; set; }
}
