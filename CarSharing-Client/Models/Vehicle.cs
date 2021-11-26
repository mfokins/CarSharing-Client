using System.ComponentModel.DataAnnotations;
using Entity.ModelData;

namespace CarSharing_Client.Models
{
    public class Vehicle
    {
        [Required] 
        public string LicenseNo { get; set; }
        [Required] 
        public string Brand { get; set; }
        [Required] 
        public string Model { get; set; }
        [Required] 
        public string Type { get; set; }
        [Required] 
        public string Transmission { get; set; }
        [Required] 
        public string FuelType { get; set; }
        [Required] 
        public int? Seats { get; set; }
        [Required] 
        public int? ManufactureYear { get; set; }
        [Required] 
        public double? Mileage { get; set; }
        public Customer Owner { get; set; }
    }
}

