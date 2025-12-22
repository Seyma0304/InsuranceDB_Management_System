namespace WebApplication.Models
{
    // Tarife (Pricing Plan/Insurance Package)
    public class TariffModel
    {
        public int TariffId { get; set; }
        public string TariffName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string CoverageType { get; set; } = string.Empty; // Health, Auto, Life, etc.
        public int DurationMonths { get; set; } // Süre (ay)
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
