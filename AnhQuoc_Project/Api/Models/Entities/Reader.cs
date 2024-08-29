using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Reader
{
    public string Id { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Fname { get; set; } = null!;

    public DateTime BoF { get; set; }

    public bool ReaderType { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual Adult? Adult { get; set; }

    public virtual Child? Child { get; set; }

    public virtual ICollection<LoanHistory> LoanHistories { get; set; } = new List<LoanHistory>();

    public virtual ICollection<LoanSlip> LoanSlips { get; set; } = new List<LoanSlip>();
}
