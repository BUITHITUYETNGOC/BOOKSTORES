using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class Supplier
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
