using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class LoanSlip
{
    public string Id { get; set; } = null!;

    public string IdUser { get; set; } = null!;

    public string IdReader { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime ExpDate { get; set; }

    public decimal LoanPaid { get; set; }

    public decimal Deposit { get; set; }

    public virtual Reader IdReaderNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<LoanDetail> LoanDetails { get; set; } = new List<LoanDetail>();
}
