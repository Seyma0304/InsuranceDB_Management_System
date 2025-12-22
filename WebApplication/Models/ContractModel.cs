namespace WebApplication.Models
{
    // Sözleşme (Contract/Policy)
    public class ContractModel
    {
        public int ContractId { get; set; }
        public int CustomerId { get; set; }
        public int TariffId { get; set; }
        public string ContractNo { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Premium { get; set; } // Prim tutarı
        public string Status { get; set; } = string.Empty; // Active, Expired, Cancelled
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties (optional - for display)
        public string? CustomerName { get; set; }
        public string? TariffName { get; set; }
    }
}
