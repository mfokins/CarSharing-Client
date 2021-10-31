using System.Collections.Generic;
using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data
{
    public interface IListingService
    {
        Task<IList<Listing>> GetAllListingsAsync(); 
        Task AddListingAsync(Listing adult);
        Task RemoveListingAsync(int id);
        Task UpdateListingAsync(Listing adult);
        
        Task<Listing> GetListingAsync(int id);
    }
}