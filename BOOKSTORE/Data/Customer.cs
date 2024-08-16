using System;
using System.Collections.Generic;

namespace BOOKSTORE.Data;

public partial class Customer
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Password { get; set; }

    public string? Image { get; set; }

    public string? Gender { get; set; }

    public DateTime? BirthDay { get; set; }

    public string? Cccd { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? StartDay { get; set; }

    public DateTime? UpdateLast { get; set; }

    public string? Account { get; set; }

    public string? Bill { get; set; }

    public string? Status { get; set; }

    public virtual Account? AccountNavigation { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
