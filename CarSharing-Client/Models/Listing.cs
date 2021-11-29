using System;
using System.ComponentModel.DataAnnotations;

namespace CarSharing_Client.Models
{
    public class Listing
    {
        
        public int Id { get; set; }
        public DateTime ListedDate { get; set; }
        
        [Required(ErrorMessage = "Please provide the date from which you want the vehicle to be available.")]
        public DateTime DateFrom { get; set; }
        
        [Required(ErrorMessage = "Please provide the date until you want the vehicle to be available.")]
        public DateTime? DateTo { get; set; }
        
        [Required(ErrorMessage = "Please provide the price for the day.")]
        public decimal? Price { get; set; }
        
        [Required(ErrorMessage = "Please provide the location of the vehicle.")]
        public string Location { get; set; }
        
        public Vehicle Vehicle { get; set; }
    }
}