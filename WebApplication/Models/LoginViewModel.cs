using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    /// <summary>
    /// ViewModel for user login in .NET Core
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// User email address for authentication
        /// </summary>
        [Required(ErrorMessage = "Email gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User password for authentication
        /// </summary>
        [Required(ErrorMessage = "Şifre gereklidir")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Remember login credentials for future sessions
        /// </summary>
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// Response model after login attempt
    /// </summary>
    public class LoginResultModel
    {
        /// <summary>
        /// Indicates whether login was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Login result message (error or success details)
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Authenticated user ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User type: Staff or Customer
        /// </summary>
        public string UserType { get; set; } = string.Empty;

        /// <summary>
        /// Redirect URL after successful login
        /// </summary>
        public string RedirectUrl { get; set; } = string.Empty;
    }
}
