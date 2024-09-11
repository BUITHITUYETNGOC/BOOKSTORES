using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOOKSTORE.Data;

public partial class Introduction
{
    [Display(Name = "ID")]
    [Required]
    public string Id { get; set; } = null!;

    [Display(Name = "Ảnh bìa đầu trang")]
    
    public string? FirstImage { get; set; }

    [Display(Name = "Ảnh trái")]
    
    public string? LeftImage { get; set; }

    [Display(Name = "Mô tả")]
    
    public string? Description { get; set; }

    [Display(Name = "Địa chỉ liên hệ")]
    [Required]
    public string? Address { get; set; }

    [Display(Name = "Số điện thoại")]
    [Required]
    public string? Phone { get; set; }

    [Display(Name = "Ngày thực hiện")]
    [Required]
    public DateTime? UpdateLast { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
