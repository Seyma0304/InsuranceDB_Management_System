using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    /// <summary>
    /// ViewModel for creating new customers (.NET Core)
    /// Supports both individual persons and company registrations
    /// </summary>
    public class CustomerCreateModel
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
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string UserPassword { get; set; } = string.Empty;

        /// <summary>
        /// Customer phone number
        /// </summary>
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Customer address
        /// </summary>
        public string AddressLine { get; set; } = string.Empty;

        /// <summary>
        /// Customer type: "Person" or "Company"
        /// </summary>
        [Required(ErrorMessage = "Müşteri türü seçilmelidir")]
        public string CustomerType { get; set; } = string.Empty;

        /// <summary>
        /// First name (for Person type customers)
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name (for Person type customers)
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// National ID number (for Person type customers)
        /// </summary>
        public string NationalIdNo { get; set; } = string.Empty;

        /// <summary>
        /// Company name (for Company type customers)
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// Company tax ID (for Company type customers)
        /// </summary>
        public string CompanyTaxNo { get; set; } = string.Empty;
    }
}