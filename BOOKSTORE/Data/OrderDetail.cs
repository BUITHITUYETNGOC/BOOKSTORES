﻿using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public string? ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
