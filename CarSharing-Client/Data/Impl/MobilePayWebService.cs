using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarSharing_Client.Data.Impl
{
    public class MobilePayWebService : IMobilePayWebService
    {
        private const string Uri = "http://localhost:8080";
        private readonly HttpClient _client;


        public MobilePayWebService()
        {
            _client = new HttpClient();
        }

        public async Task<string> CreateNewPayment(decimal amount)
        {
            NumberFormatInfo format = new()
            {
                NumberDecimalSeparator = "."
            };

            HttpResponseMessage responseMessage =
                await _client.GetAsync(Uri + $"/mobilepay?amount={amount.ToString(format)}");
            
            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
            
            return await responseMessage.Content.ReadAsStringAsync();
        }
        
        public async Task<string> GenerateQrCode(int qrcodeWidth, string text)
        {
            WebClient webClient = new();
            byte[] imageBytes = await webClient.DownloadDataTaskAsync($"https://chart.apis.google.com/chart?cht=qr&chs={qrcodeWidth}x{qrcodeWidth}&chl={text}");
           
            return Convert.ToBase64String(imageBytes);
        }
        public async Task<bool> ValidatePayment(string paymentId)
        {
            HttpResponseMessage responseMessage =
                await _client.GetAsync(Uri + $"/mobilepay/verify?paymentId={paymentId}");


            if (!responseMessage.IsSuccessStatusCode)
            {
                var jsonObj = await JsonDocument.ParseAsync(await responseMessage.Content.ReadAsStreamAsync());
                throw new Exception(jsonObj.RootElement.GetProperty("message").GetString());
            }
            
            string result = await responseMessage.Content.ReadAsStringAsync();
            bool finalResult = bool.Parse(result);
            return finalResult;
        }
    }
}