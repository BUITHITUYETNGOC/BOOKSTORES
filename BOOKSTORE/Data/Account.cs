using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string Name { get; set; }

    public string Password { get; set; }
    public string? Role { get; set; }
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
