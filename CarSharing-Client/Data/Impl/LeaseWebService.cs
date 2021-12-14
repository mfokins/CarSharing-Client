using System;
using System.Collections;
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
    public class LeaseWebService : ILeaseService
    {
        private readonly HttpClient _client;
        private const string Uri = "http://localhost:8080";
       

        public LeaseWebService()
        {
            _client = new HttpClient();
        }

        public async Task<Lease> ValidateLeaseAsync(Lease lease, string couponCode)
        {
            string listingAsJson = JsonSerializer.Serialize(lease, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new DateTimeConverter()}
            });

            HttpContent content = new StringContent(listingAsJson,
                Encoding.UTF8,
                "application/json"
            );

            string pathUri = "/leases/coupons/" + (couponCode != "" ? $"{couponCode}" : "default");

            HttpResponseMessage responseMessage = await _client.PostAsync(Uri + pathUri, content);
            
            var resultString = await responseMessage.Content.ReadAsStringAsync();
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
            
            var result = JsonSerializer.Deserialize<Lease>(resultString, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return result;
            
        }

        public async Task AddLeaseAsync(Lease lease)
        {
            string listingAsJson = JsonSerializer.Serialize(lease, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new DateTimeConverter()}
            });

            HttpContent content = new StringContent(listingAsJson,
                Encoding.UTF8,
                "application/json"
            );

           
            HttpResponseMessage responseMessage = await _client.PostAsync(Uri + "/leases", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
        }

        

        public async Task CancelLeaseAsync(int id)
        {
           
            var responseMessage = await _client.DeleteAsync($"{Uri}/leases/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
        }

        public async Task<IList<Lease>> GetLeasesByListingAsync(int listingId)
        {
           
            
            HttpResponseMessage responseMessage =
                await _client.GetAsync(Uri + $"/leases/listing?id={listingId}");
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }

            string result = await responseMessage.Content.ReadAsStringAsync();

            IList<Lease> leases = JsonSerializer.Deserialize<IList<Lease>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new DateTimeConverter()}
            });

            return leases;
        }
        
        public async Task<Lease> GetLeaseById(int id)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(Uri + $"/leases/{id}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }

            string result = await responseMessage.Content.ReadAsStringAsync();

            Lease lease = JsonSerializer.Deserialize<Lease>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return lease;
        }

        public async Task<IList<Lease>> GetLeasesByCustomerCpr(string customerCpr)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(Uri + $"/leases/customer?cpr={customerCpr}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }

            string result = await responseMessage.Content.ReadAsStringAsync();

            IList<Lease> leases = JsonSerializer.Deserialize<IList<Lease>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return leases;
        }
    }
        
    
}