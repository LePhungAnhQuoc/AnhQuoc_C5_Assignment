using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Book
{
    public int Id { get; set; }

    public string Isbn { get; set; } = null!;

    public string IdPublisher { get; set; } = null!;

    public string IdTranslator { get; set; } = null!;

    public string Language { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    public decimal Price { get; set; }

    public decimal PriceCurrent { get; set; }

    public string? Note { get; set; }

    public string IdBookStatus { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual BookStatus IdBookStatusNavigation { get; set; } = null!;

    public virtual Publisher IdPublisherNavigation { get; set; } = null!;

    public virtual Translator IdTranslatorNavigation { get; set; } = null!;

    public virtual BookIsbn IsbnNavigation { get; set; } = null!;

    public virtual ICollection<LoanDetailHistory> LoanDetailHistories { get; set; } = new List<LoanDetailHistory>();

    public virtual ICollection<LoanDetail> LoanDetails { get; set; } = new List<LoanDetail>();
}
