using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Child
{
    public string IdReader { get; set; } = null!;

    public string IdAdult { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual Adult IdAdultNavigation { get; set; } = null!;

    public virtual Reader IdReaderNavigation { get; set; } = null!;
}
