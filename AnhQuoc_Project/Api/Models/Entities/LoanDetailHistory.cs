using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class LoanDetailHistory
{
    public string Id { get; set; } = null!;

    public string IdLoanHistory { get; set; } = null!;

    public int IdBook { get; set; }

    public DateTime ExpDate { get; set; }

    public decimal PaidMoney { get; set; }

    public string? Note { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual LoanHistory IdLoanHistoryNavigation { get; set; } = null!;
}
