using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class History
{
    public string Id { get; set; } = null!;

    public string? CustomerId { get; set; }

    public DateTime? DayTh { get; set; }

    public string? Content { get; set; }

    public virtual Introduction? ContentNavigation { get; set; }

    public virtual Customer? Customer { get; set; }
}
