using System.ComponentModel.DataAnnotations;

namespace BOOKSTORE.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "🖋️ Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "🖋️ Địa chỉ Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "🖋️ Vui lòng nhập Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "🖋️ Vui lòng nhập lại Mật khẩu")]
        [Compare("Password", ErrorMessage = "🖋️ Mật khẩu không khớp")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "🖋️ Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "🖋️Địa chỉ Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "🖋️ Vui lòng nhập Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
