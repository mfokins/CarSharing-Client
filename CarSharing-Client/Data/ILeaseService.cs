using System.Collections.Generic;
using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data
{
    public interface ILeaseService
    {

        Task<Lease> ValidateLeaseAsync(Lease lease, string couponCode);
        
        Task AddLeaseAsync(Lease lease);

        Task CancelLeaseAsync(int id);
        Task<IList<Lease>> GetLeasesByListingAsync(int listingId);
        Task<Lease> GetLeaseById(int id);

        Task<IList<Lease>> GetLeasesByCustomerCpr(string customerCpr);
    }
}