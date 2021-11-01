using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data.Impl
{
    public class VehicleWebService : IVehicleService
    {
        private const string Uri = "http://localhost:8080";
        private readonly HttpClient _client;

        public VehicleWebService()
        {
            _client = new HttpClient();
        }
        
        public async Task<IList<Vehicle>> GetAllVehiclesAsync()
        {
            Task<string> stringAsync = _client.GetStringAsync(Uri + "/vehicles");
            string message = await stringAsync;
            List<Vehicle> result = JsonSerializer.Deserialize<List<Vehicle>>(message);
            return result;
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            string vehicleAsJson = JsonSerializer.Serialize(vehicle);
            HttpContent content = new StringContent(vehicleAsJson,
                Encoding.UTF8,
                "application/json");
            await _client.PostAsync(Uri + "/Vehicles", content);
        }

        public async Task RemoveVehicleAsync(string licenseNo)
        {
            await _client.DeleteAsync($"{Uri}/Vehicle/{licenseNo}");
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            string vehicleAsJson = JsonSerializer.Serialize(vehicle);
            HttpContent content = new StringContent(vehicleAsJson,
                Encoding.UTF8,
                "application/json");
            await _client.PatchAsync($"{Uri}/Vehicles/{vehicle.LicenseNo}", content);        }

        public async Task<Vehicle> GetVehicleAsync(string licenseNo)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(Uri + $"/vehicle?licenseNo={licenseNo}");
            
            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            
            string result = await responseMessage.Content.ReadAsStringAsync();

            Vehicle vehicle = JsonSerializer.Deserialize<Vehicle>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return vehicle;
        }
    }
}