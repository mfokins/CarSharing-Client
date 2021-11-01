using System;
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
        private const string Uri = "http://localhost:8080";
        private readonly HttpClient _client;

        public ListingWebService()
        {
            _client = new HttpClient();
        }
        public async Task AddListingAsync(Listing listing)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveListingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateListingAsync(Listing listing)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Listing>> GetListingsAsync(string location, DateTime dateFrom, DateTime dateTo)
        {
            HttpResponseMessage responseMessage = 
                await _client.GetAsync(Uri + $"/listings?location={location}&dateFrom={dateFrom:yyyy-MM-ddTHH:mm:ssZ}&dateTo={dateTo:yyyy-MM-ddTHH:mm:ssZ}");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");

            string result = await responseMessage.Content.ReadAsStringAsync();

            IList<Listing> listings = JsonSerializer.Deserialize<IList<Listing>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return listings;
        }
    }
}