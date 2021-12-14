using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data.Impl
{
    public class UserService : IUserService
    {
        private const string Uri = "http://localhost:8080";
        private readonly HttpClient _client;

        public UserService()
        {
            _client = new HttpClient();
        }


        public async Task<Customer> ValidateCustomer(string username, string password)
        {
            Account account = new Account()
            {
                Username = username,
                Password = password
            };

            string accountAsJson = JsonSerializer.Serialize(account, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            StringContent accountAsStringContent = new StringContent(
                accountAsJson,
                Encoding.UTF8,
                "application/json"
            );

            var responseMessage = await _client.PostAsync($"{Uri}/session", accountAsStringContent);

            
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }

            var resultString = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Customer>(resultString, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return result;
        }

        public async Task RegisterCustomer(Account account)
        {
            string accountAsJson = JsonSerializer.Serialize(account, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            StringContent accountAsStringContent = new StringContent(
                accountAsJson,
                Encoding.UTF8,
                "application/json"
            );

            var responseMessage = await _client.PostAsync($"{Uri}/accounts", accountAsStringContent);

            
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
        }
    }
}