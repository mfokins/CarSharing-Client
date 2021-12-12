using System.ComponentModel.DataAnnotations;

namespace CarSharing_Client.Models
{
    public class Customer
    {
        public string Cpr { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        
        public int AccessLevel { get; set; } // admin lvl 3
    }
}