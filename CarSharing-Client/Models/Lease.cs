using System;


namespace CarSharing_Client.Models
{
    public class Lease
    {
        public int Id { get; set; }
       
        public DateTime LeasedFrom { get; set; }
       
        public DateTime LeasedTo { get; set; }
        
        public bool Canceled { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public Listing Listing { get; set; }
        
        public Customer Customer { get; set; }
    }
}