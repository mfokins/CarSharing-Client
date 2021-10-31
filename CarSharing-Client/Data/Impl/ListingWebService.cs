using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data.Impl
{
    public class ListingWebService : IListingService
    {
        private string uri = "https://localhost:5002";
        private readonly HttpClient client;

        public ListingWebService()
        {
            client = new HttpClient();
        }

        public async Task<IList<Listing>> GetAllListingsAsync()
        {
            Task<string> stringAsync = client.GetStringAsync(uri + "/Listings");
            string message = await stringAsync;
            List<Listing> result = JsonSerializer.Deserialize<List<Listing>>(message);
            return result;
        }

        public async Task AddListingAsync(Listing listing)
        {
            string listingAsJson = JsonSerializer.Serialize(listing);
            HttpContent content = new StringContent(listingAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PostAsync(uri + "/Listings", content);
        }

        public async Task RemoveListingAsync(int id)
        {
            await client.DeleteAsync($"{uri}/Listings/{id}");
        }

        public async Task UpdateListingAsync(Listing listing)
        {
            string listingAsJson = JsonSerializer.Serialize(listing);
            HttpContent content = new StringContent(listingAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PatchAsync($"{uri}/Listings/{listing.Price}", content); //TODO CHANGE TO ID IN FUTURE!
        }

        public async Task<Listing> GetListingAsync(int id)
        {
            Task<string> stringAsync = client.GetStringAsync($"{uri}/Listings/{id}");
            string message = await stringAsync;
            Listing result = JsonSerializer.Deserialize<Listing>(message);
            return result;        }
    }
}