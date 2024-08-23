using BOOKSTORE.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOOKSTORE.Data;

public partial class Customer
{
    [Display(Name="Mã KH")]
    [Required]
    public string Id { get; set; } = null!;
    
    [Display(Name = "Họ tên")]
    [Required]
    public string? Name { get; set; }

    [Display(Name = "Mật khẩu")]
    [Required]
    public string? Password { get; set; }

    [Display(Name = "Ảnh")]
    [Required]
    public string? Image { get; set; }

    [Display(Name = "Giới tính")]
    [Required]
    public string? Gender { get; set; }

    [Display(Name = "Ngày sinh")]
    [Required]
    public DateTime? BirthDay { get; set; }

    [Display(Name = "CCCD")]
    public string? Cccd { get; set; }

    [Display(Name = "Địa chỉ nhận hàng")]
    [Required]
    public string? Address { get; set; }

  
    [Required]
    public string? Email { get; set; }

    [Display(Name = "Số điện thoại")]
    [Required]
    public string? Phone { get; set; }

    [Display(Name = "Ngày đăng ký")]
    [Required]
    public DateTime? StartDay { get; set; }

    [Display(Name = "Ngày thực hiện")]
    [Required]
    public DateTime? UpdateLast { get; set; }

    [Display(Name = "Loại tài khoản")]
    [Required]
    public string? Account { get; set; }
    
    
    
    public string? Bill { get; set; }
    public string? Status { get; set; }

    public virtual Account? AccountNavigation { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
