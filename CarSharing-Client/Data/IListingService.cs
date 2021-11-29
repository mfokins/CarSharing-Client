using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data
{
    public interface IListingService
    {
        Task AddListingAsync(Listing listing);
        Task RemoveListingAsync(int id);
        Task UpdateListingAsync(Listing listing);
        Task<IList<Listing>> GetListingsAsync(string location, DateTime dateFrom, DateTime dateTo);
        Task<Listing> GetListingsByVehicleAsync(string licenseNo);
        Task<Listing> GetListingById(int id);

    }
}