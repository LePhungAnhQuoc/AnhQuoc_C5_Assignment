using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Province
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }
}
