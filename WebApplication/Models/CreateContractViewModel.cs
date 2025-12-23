using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    /// <summary>
    /// ViewModel for creating new insurance contracts in .NET Core
    /// </summary>
    public class CreateContractViewModel
    {
        /// <summary>
        /// Selected customer ID for the contract
        /// </summary>
        [Required(ErrorMessage = "Müşteri seçilmelidir")]
        public int CustomerId { get; set; }
        
        /// <summary>
        /// Selected tariff/plan ID for the contract
        /// </summary>
        [Required(ErrorMessage = "Tarife seçilmelidir")]
        public int TariffId { get; set; }
        
        /// <summary>
        /// Contract start date
        /// </summary>
        [Required(ErrorMessage = "Başlangıç tarihi gereklidir")]
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Contract duration in months (auto-populated from tariff)
        /// </summary>
        public int DurationMonths { get; set; }
        
        /// <summary>
        /// Premium amount (auto-calculated from tariff)
        /// </summary>
        public decimal Premium { get; set; }
        
        /// <summary>
        /// List of available customers for dropdown selection
        /// </summary>
        public List<CustomerListItem> CustomerList { get; set; } = new();
        
        /// <summary>
        /// List of available tariffs for dropdown selection
        /// </summary>
        public List<TariffListItem> TariffList { get; set; } = new();
    }
    
    /// <summary>
    /// Represents a customer for list/dropdown display
    /// </summary>
    public class CustomerListItem
    {
        /// <summary>
        /// Unique customer identifier
        /// </summary>
        public int CustomerId { get; set; }
        
        /// <summary>
        /// Display name (person first+last or company name)
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        
        /// <summary>
        /// Customer type (Person or Company)
        /// </summary>
        public string CustomerType { get; set; } = string.Empty;
    }
    
    /// <summary>
    /// Represents a tariff plan for list/dropdown display
    /// </summary>
    public class TariffListItem
    {
        /// <summary>
        /// Unique tariff identifier
        /// </summary>
        public int TariffId { get; set; }
        
        /// <summary>
        /// Tariff plan name
        /// </summary>
        public string TariffName { get; set; } = string.Empty;
        
        /// <summary>
        /// Base price/premium amount
        /// </summary>
        public decimal BasePrice { get; set; }
        
        /// <summary>
        /// Duration of the tariff in months
        /// </summary>
        public int DurationMonths { get; set; }
    }
}
