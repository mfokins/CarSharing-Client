using System.ComponentModel.DataAnnotations;

namespace CarSharing_Client.Models
{
    public class Account
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public Customer Customer { get; set; }
    }
}