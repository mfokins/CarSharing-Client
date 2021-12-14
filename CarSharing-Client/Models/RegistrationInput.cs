using System.ComponentModel.DataAnnotations;

namespace CarSharing_Client.Models
{
    public class RegistrationInput
    {
        [Required(ErrorMessage = "Please provide the username.")]
        [StringLength(255, MinimumLength = 3)]
        public string Username { get; set; }
        
        
        [Required(ErrorMessage = "Please provide the password.")]
        [StringLength(255, MinimumLength = 1)]
        public string Password { get; set; }
        
        
        [Required(ErrorMessage = "Please provide your cpr in the format 'ddMMyyxxxx'.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please provide your cpr in the format 'ddMMyyxxxx'.")]
        public string Cpr { get; set; }
        
        
        [Required(ErrorMessage = "Please provide your firstname.")]
        [StringLength(255, MinimumLength = 3)]
        public string FirstName { get; set; }
        
        
        [Required(ErrorMessage = "Please provide your lastname.")]
        [StringLength(255, MinimumLength = 3)]
        public string LastName { get; set; }
        
        
        [Required(ErrorMessage = "Please provide your phone number.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "The phone number must be in format '00 00 00 00'.")]
        public string PhoneNo { get; set; }
    }
}