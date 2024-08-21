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
        [Required(ErrorMessage = @"Vui lòng không b? tr?ng.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = @"Vui lòng không b? tr?ng.")]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        [Required(ErrorMessage = @"Vui lòng không b? tr?ng.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = @"Vui lòng không b? tr?ng.")]
        public string Password { get; set; } = string.Empty;
    }

}
