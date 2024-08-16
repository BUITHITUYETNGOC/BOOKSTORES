using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class Order
{
    public int OrderId { get; set; }

    public string? CustomerId { get; set; }

    public string? CartId { get; set; }

    public int? Quantity { get; set; }

    public double? UnitPrice { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual OrderDetail? OrderDetail { get; set; }
}
