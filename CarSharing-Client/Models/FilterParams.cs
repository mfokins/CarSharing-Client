namespace CarSharing_Client.Models
{
    public class FilterParams
    {
        public string CarType { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public int? NumberOfSeats { get; set; }
    }
}