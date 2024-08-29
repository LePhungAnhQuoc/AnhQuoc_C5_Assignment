using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class BookIsbn
{
    public string Isbn { get; set; } = null!;

    public string IdBookTitle { get; set; } = null!;

    public string IdAuthor { get; set; } = null!;

    public string OriginLanguage { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual Author IdAuthorNavigation { get; set; } = null!;

    public virtual BookTitle IdBookTitleNavigation { get; set; } = null!;
}
