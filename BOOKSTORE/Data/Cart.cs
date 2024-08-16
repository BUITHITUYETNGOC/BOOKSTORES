using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class Cart
{
    public string Id { get; set; } = null!;

    public string? ProductsId { get; set; }

    public string? UnitPrice { get; set; }

    public string? Sl { get; set; }

    public string? Image { get; set; }

    public string? Pay { get; set; }

    public DateTime? UpdateLast { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
