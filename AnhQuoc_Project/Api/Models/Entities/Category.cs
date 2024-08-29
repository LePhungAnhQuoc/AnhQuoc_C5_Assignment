using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual ICollection<BookTitle> BookTitles { get; set; } = new List<BookTitle>();
}
