namespace WebApplication.Models
{
    public class CustomerCreateModel
    {
        // User info
        public string Email { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;

        // Customer info
        public string Phone { get; set; } = string.Empty;

        // Address
        public string AddressLine { get; set; } = string.Empty;

        // Customer type
        public string CustomerType { get; set; } = string.Empty;  // "Person" or "Company"

        // Person fields
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalIdNo { get; set; } = string.Empty;

        // Company fields
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyTaxNo { get; set; } = string.Empty;
    }
}