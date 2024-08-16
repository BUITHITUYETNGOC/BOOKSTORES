using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
