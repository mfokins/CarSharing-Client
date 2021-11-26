using System;
using System.Text.Json.Serialization;

namespace CarSharing_Client.Models
{
    public class Listing
    {
        
        public DateTime ListedDate { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}