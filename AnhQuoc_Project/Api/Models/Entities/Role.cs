using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Group { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<RoleFunction> RoleFunctions { get; set; } = new List<RoleFunction>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
