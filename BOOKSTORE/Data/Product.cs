using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOOKSTORE.Data;

public partial class Product
{
    [Display(Name="Mã sản phẩm")]
    [Required]
    public string Id { get; set; } = null!;

    [Display(Name = "Tên sản phẩm")]
    [Required]
    public string? Name { get; set; }

    [Display(Name = "Giá bán")]
    [Range(1000, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 1000")]
    [Required]
    public double? UnitPrice { get; set; }

    [Display(Name = "Thể loại")]
    [Required]
    public string? CategoryId { get; set; }

    [Display(Name = "Mã nhà cung cấp")]
    [Required]
    public string? SupplierId { get; set; }

    [Display(Name = "Số lượng")]
    [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
    [Required]
    public int? Sl { get; set; }

    [Display(Name = "Mô tả")]
    [Required]
    public string? Description { get; set; }

    [Display(Name = "Tác giả")]
    [Required]
    public string? Author { get; set; }

    [Display(Name = "Trạng thái")]
    [Required]
    public string? Status { get; set; }

    
    public DateTime? UpdateLast { get; set; }

    public string? Image { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }
}
