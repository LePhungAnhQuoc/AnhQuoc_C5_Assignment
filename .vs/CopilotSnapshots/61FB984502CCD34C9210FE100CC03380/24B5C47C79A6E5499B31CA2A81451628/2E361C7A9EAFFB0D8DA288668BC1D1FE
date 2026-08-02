using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Function
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? IdParent { get; set; }

    public string? UrlImage { get; set; }

    public bool IsAdmin { get; set; }

    public bool Status { get; set; }

    public virtual Function? IdParentNavigation { get; set; }

    public virtual ICollection<Function> InverseIdParentNavigation { get; set; } = new List<Function>();

    public virtual ICollection<RoleFunction> RoleFunctions { get; set; } = new List<RoleFunction>();
}
