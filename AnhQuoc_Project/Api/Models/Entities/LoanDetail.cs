using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class LoanDetail
{
    public string Id { get; set; } = null!;

    public string IdLoan { get; set; } = null!;

    public int IdBook { get; set; }

    public DateTime ExpDate { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual LoanSlip IdLoanNavigation { get; set; } = null!;
}
