using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class BookTitle
{
    public string Id { get; set; } = null!;

    public string IdCategory { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Note { get; set; }

    public string Summary { get; set; } = null!;

    public string UrlImage { get; set; } = null!;

    public virtual ICollection<BookIsbn> BookIsbns { get; set; } = new List<BookIsbn>();

    public virtual Category IdCategoryNavigation { get; set; } = null!;
}
