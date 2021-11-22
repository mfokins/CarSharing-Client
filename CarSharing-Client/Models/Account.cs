using System.ComponentModel.DataAnnotations;

namespace Entity.ModelData
{
    public class Account
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public Customer Customer { get; set; }
    }
}