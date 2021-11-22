using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarSharing_Client.Models;
using Entity.ModelData;

namespace CarSharing_Client.Data.Impl
{
    public class UserService : IUserService
    {
        private const string Uri = "http://10.154.212.114:8080";
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

            string AccountAsJson = JsonSerializer.Serialize(account, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            StringContent AccountAsstringContent = new StringContent(
                AccountAsJson,
                Encoding.UTF8,
                "application/json");

            Console.WriteLine("id 0");

            var response = await _client.PostAsync($"{Uri}/session", AccountAsstringContent);

            Console.WriteLine("id 0.5");

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            
            Console.WriteLine("id 2");
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Customer>(resultString, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            Console.WriteLine("id 3");

            return result;

        }
    }
}

        
    