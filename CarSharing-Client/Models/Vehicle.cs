using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Entity.ModelData;

namespace CarSharing_Client.Models
{
    public class Vehicle
    {
        [Key] 
        public string LicenseNo { get; set; }
        [Required] 
        public string Brand { get; set; }
        [Required] 
        public string Model { get; set; }
        public string Type { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public int Seats { get; set; }
        public int ManufactureYear { get; set; }
        public double Mileage { get; set; }

        public Customer Owner { get; set; }
    }

    public static class VehicleTransmission
    {
        public const string Manual = "Manual transmission";
        public const string Automatic = "Automatic transmission";
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
    }
}

