using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOOKSTORE.Data;

public partial class Category
{
    [Display(Name = "Mã loại")]
    [Required]
    public string Id { get; set; } = null!;

    [Display(Name = "Tên loại")]
    [Required]
    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
