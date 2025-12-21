namespace WebApplication.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}