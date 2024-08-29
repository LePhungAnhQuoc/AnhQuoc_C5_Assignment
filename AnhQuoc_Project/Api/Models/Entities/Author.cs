using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Author
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime BoF { get; set; }

    public string Summary { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual ICollection<BookIsbn> BookIsbns { get; set; } = new List<BookIsbn>();
}
