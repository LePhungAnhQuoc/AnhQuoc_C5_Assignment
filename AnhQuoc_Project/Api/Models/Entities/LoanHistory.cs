using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class LoanHistory
{
    public string Id { get; set; } = null!;

    public string IdUser { get; set; } = null!;

    public string IdReader { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime ExpDate { get; set; }

    public decimal LoanPaid { get; set; }

    public decimal Deposit { get; set; }

    public decimal FineMoney { get; set; }

    public decimal Total { get; set; }

    public string? Note { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual Reader IdReaderNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<LoanDetailHistory> LoanDetailHistories { get; set; } = new List<LoanDetailHistory>();
}
