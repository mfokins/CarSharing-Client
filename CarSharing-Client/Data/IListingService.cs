using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data
{
    public interface IListingService
    {
        Task AddListingAsync(Listing adult);
        Task RemoveListingAsync(int id);
        Task UpdateListingAsync(Listing adult);
        Task<IList<Listing>> GetListingsAsync(string location, DateTime dateFrom, DateTime dateTo);
    }
}