using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class UserRole
{
    public string Id { get; set; } = null!;

    public string IdUser { get; set; } = null!;

    public string IdRole { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
