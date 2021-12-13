using System.ComponentModel.DataAnnotations;

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
        
        // is approved by the admin
        public bool IsApproved { get; set; }
        
        public static class VehicleTransmission
        {
            public const string Manual = "Manual";
            public const string Automatic = "Automatic";
        }

        public static class VehicleType
        {
            public const string Van = "Van";
            public const string Suv = "SUV";
            public const string Sedan = "Sedan";
            public const string Coupe = "Coupe";
            public const string Hatchback = "Hatchback";
            public const string PickupTruck = "Pickup Truck";
        }

        public static class VehicleFuelType
        {
            public const string Electric = "Electric";
            public const string Diesel = "Diesel";
            public const string Petrol = "Petrol";
            public const string Hybrid = "Hybrid";
            public const string Hydrogen = "Hydrogen";
        }
    }
}

