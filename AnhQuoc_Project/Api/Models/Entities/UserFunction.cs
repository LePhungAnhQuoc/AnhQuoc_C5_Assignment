using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class UserFunction
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? IdParent { get; set; }

    public string? UrlImage { get; set; }

    public bool IsAdmin { get; set; }

    public bool Status { get; set; }

    public virtual UserFunction? IdParentNavigation { get; set; }

    public virtual ICollection<UserFunction> InverseIdParentNavigation { get; set; } = new List<UserFunction>();

    public virtual ICollection<RoleFunction> RoleFunctions { get; set; } = new List<RoleFunction>();
}
