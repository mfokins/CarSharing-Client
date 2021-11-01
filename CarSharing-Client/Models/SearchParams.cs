using System;
using System.ComponentModel.DataAnnotations;

namespace CarSharing_Client.Models
{
    public class SearchParams
    {
        [Required(ErrorMessage = "Please provide the location.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please provide the start date.")]
        public DateTime? DateFrom { get; set; }
        [Required(ErrorMessage = "Please provide the end date.")]
        public DateTime? DateTo { get; set; }
    }
}