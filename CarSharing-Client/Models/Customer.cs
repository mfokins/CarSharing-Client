using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.ModelData
{
    public class Customer
    {
        [Key]
        public string Cpr { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
    }
}