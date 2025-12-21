using System;

namespace WebApplication.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}