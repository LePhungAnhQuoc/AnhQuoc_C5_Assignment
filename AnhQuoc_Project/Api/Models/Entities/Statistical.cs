using System;
using System.Collections.Generic;

namespace Api.Models.Entities;

public partial class Statistical
{
    public DateTime DateTime { get; set; }

    public string Data { get; set; } = null!;

    public string Description { get; set; } = null!;
}
