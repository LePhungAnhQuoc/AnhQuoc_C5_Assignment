using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class UserInfo
{
    public string IdUser { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Fname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
