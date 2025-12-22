namespace WebApplication.Models
{
    // Login için ViewModel
    public class LoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
    
    // Login sonuç
    public class LoginResultModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserType { get; set; } = string.Empty; // Staff, Customer
        public string RedirectUrl { get; set; } = string.Empty;
    }
}
