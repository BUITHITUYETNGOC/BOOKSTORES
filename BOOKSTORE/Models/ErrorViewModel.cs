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
        [DataType(DataType.EmailAddress,ErrorMessage = @"Vui lòng Nhập Email")]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password, ErrorMessage = @"Vui lòng không b? tr?ng.")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = @"Mật Khẩu Không Khớp.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage = @"Vui lòng Nhập Email")]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password, ErrorMessage = @"Vui lòng không b? tr?ng.")]
        public string Password { get; set; } = string.Empty;
         
    }

}
