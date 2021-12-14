using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarSharing_Client.Extensions;
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
            string listingAsJson = JsonSerializer.Serialize(listing, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new DateTimeConverter()}
            });

            HttpContent content = new StringContent(listingAsJson,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage responseMessage = await _client.PostAsync(Uri + "/listings", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
        }

        public async Task RemoveListingAsync(int id)
        {
            var responseMessage = await _client.DeleteAsync($"{Uri}/listings/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
        }

        public async Task UpdateListingAsync(Listing listing)
        {
            string listingAsJson = JsonSerializer.Serialize(listing, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new DateTimeConverter()}
            });
            
            HttpContent content = new StringContent(listingAsJson,
                Encoding.UTF8,
                "application/json");
            
            await _client.PatchAsync($"{Uri}/listings/{listing.Id}", content);
        }

        public async Task<IList<Listing>> GetListingsAsync(string location, DateTime dateFrom, DateTime dateTo)
        {
            HttpResponseMessage responseMessage =
                await _client.GetAsync(Uri + $"/listings?location={location}&dateFrom={dateFrom:yyyy-MM-ddTHH':'mm':'ssZ}&dateTo={dateTo:yyyy-MM-ddTHH':'mm':'ssZ}");
            
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
            

            string result = await responseMessage.Content.ReadAsStringAsync();

            IList<Listing> listings = JsonSerializer.Deserialize<IList<Listing>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new DateTimeConverter()}
            });

            if (listings != null && !listings.Any())
                throw new Exception("No listings found for this location and dates");
            return listings;
        }

        public async Task<Listing> GetListingsByVehicleAsync(string licenseNo)
        {
            HttpResponseMessage responseMessage =
                await _client.GetAsync(Uri + $"/listings/vehicle?licenseNo={licenseNo}");


            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }

            string result = await responseMessage.Content.ReadAsStringAsync();

            IList<Listing> listings = JsonSerializer.Deserialize<IList<Listing>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new DateTimeConverter()}
            });
            
            if (listings == null || !listings.Any())
                return null;
            
            return listings.First();
        }

        public async Task<Listing> GetListingById(int id)
        {
            HttpResponseMessage responseMessage =
                await _client.GetAsync(Uri + $"/listings/{id}");
            

            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }

            string result = await responseMessage.Content.ReadAsStringAsync();

            Listing listing = JsonSerializer.Deserialize<Listing>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new DateTimeConverter()}
            });
            return listing;
        }
    }
}