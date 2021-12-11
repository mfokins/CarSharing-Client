using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarSharing_Client.Data.Impl
{
    public class MobilePayWebService : IMobilePayWebService
    {
        //private const string Uri = "http://10.154.212.101:8080";
        private const string Uri = "http://localhost:8080";
        private readonly HttpClient _client;


        public MobilePayWebService()
        {
            _client = new HttpClient();
        }

        public async Task<string> CreateNewPayment()
        {
            HttpResponseMessage responseMessage =
                await _client.GetAsync(Uri + "/mobilepay/");
            
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                Console.WriteLine(responseMessage);
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
            

            string result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<bool> ValidatePayment()
        {
            HttpResponseMessage responseMessage =
                await _client.GetAsync(Uri + "/mobilepay/verify");
            
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                Console.WriteLine(responseMessage);
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
            

            string result = await responseMessage.Content.ReadAsStringAsync();
            bool finalResult = bool.Parse(result);
            return finalResult;
        }
    }
}