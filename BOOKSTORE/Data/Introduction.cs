using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class Introduction
{
    public string Id { get; set; } = null!;

    public string? FirstImage { get; set; }

    public string? LeftImage { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public DateTime? UpdateLast { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
