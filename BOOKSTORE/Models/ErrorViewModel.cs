namespace BOOKSTORE.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class RegisterModel
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}
