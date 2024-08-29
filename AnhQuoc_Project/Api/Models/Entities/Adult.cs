using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Adult
{
    public string IdReader { get; set; } = null!;

    public string? Identify { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime ExpireDate { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual Reader IdReaderNavigation { get; set; } = null!;
}
