namespace WebApplication.Models
{
    public class PolicyModel
    {
        public int PolicyId { get; set; }
        public string PolicyNo { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public string TariffName { get; set; } = string.Empty;
        public string TariffCode { get; set; } = string.Empty;
        public string ContractName { get; set; } = string.Empty;
        public string ContractNo { get; set; } = string.Empty;
    }
}
