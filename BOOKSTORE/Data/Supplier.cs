using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOOKSTORE.Data;

public partial class Supplier
{
    [Display(Name="Mã nhà cung cấp")]
    [Required]
    public string Id { get; set; } = null!;
    
    [Display(Name = "Tên nhà cung cấp")]
    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Email { get; set; }
    
    [Display(Name = "Số điện thoại")]
    [Required]
    public string? Phone { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
