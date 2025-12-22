using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    // Yeni sözleşme oluşturma için ViewModel
    public class CreateContractViewModel
    {
        [Required(ErrorMessage = "Müşteri seçilmelidir")]
        public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "Tarife seçilmelidir")]
        public int TariffId { get; set; }
        
        [Required(ErrorMessage = "Başlangıç tarihi gereklidir")]
        public DateTime StartDate { get; set; }
        
        public int DurationMonths { get; set; } // Tarife'den otomatik gelecek
        
        public decimal Premium { get; set; } // Tarife'den otomatik hesaplanacak
        
        // Dropdown için listeleme
        public List<CustomerListItem>? CustomerList { get; set; }
        public List<TariffListItem>? TariffList { get; set; }
    }
    
    public class CustomerListItem
    {
        public int CustomerId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string CustomerType { get; set; } = string.Empty;
    }
    
    public class TariffListItem
    {
        public int TariffId { get; set; }
        public string TariffName { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int DurationMonths { get; set; }
    }
}
