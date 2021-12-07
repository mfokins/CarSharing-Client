using System.ComponentModel.DataAnnotations;

namespace CarSharing_Client.Models
{
    public class Account
    {
       //TODO by Tomas go over client model, there are some things that should probably only be on the database tier
        
        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public Customer Customer { get; set; }
    }
}