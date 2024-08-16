using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public double? UnitPrice { get; set; }

    public string? CategoryId { get; set; }

    public string? SupplierId { get; set; }

    public int? Sl { get; set; }

    public string? Description { get; set; }

    public string? Author { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdateLast { get; set; }

    public string? Image { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }
}
