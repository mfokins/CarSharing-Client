using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entity.ModelData;

namespace CarSharing_Client.Data.Impl
{
    public class UserService : IUserService
    {
        private const string Uri = "http://10.154.212.129:8080";
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

            var response = await _client.PostAsync($"{Uri}/session", accountAsStringContent);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Customer>(resultString, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return result;
        }
    }
}