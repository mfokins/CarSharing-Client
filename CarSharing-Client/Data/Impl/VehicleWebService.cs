using System;
using System.Collections;
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
        private const string Uri = "http://10.154.212.86:8080";
        private readonly HttpClient _client;

        public VehicleWebService()
        {
            _client = new HttpClient();
        }


        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            string vehicleAsJson = JsonSerializer.Serialize(vehicle, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            HttpContent content = new StringContent(vehicleAsJson,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage responseMessage = await _client.PostAsync(Uri + "/vehicles", content);
            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
        }

        public async Task RemoveVehicleAsync(string licenseNo)
        {
            var response = await _client.DeleteAsync($"{Uri}/vehicles/{licenseNo}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            string vehicleAsJson = JsonSerializer.Serialize(vehicle, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            HttpContent content = new StringContent(vehicleAsJson,
                Encoding.UTF8,
                "application/json");
            await _client.PatchAsync($"{Uri}/vehicles/{vehicle.LicenseNo}", content);
        }

        public async Task<Vehicle> GetVehicleAsync(string licenseNo)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(Uri + $"/vehicles?licenseNo={licenseNo}");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");

            string result = await responseMessage.Content.ReadAsStringAsync();

            Vehicle vehicle = JsonSerializer.Deserialize<Vehicle>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return vehicle;
        }

        public async Task<IList<Vehicle>> GetVehiclesByOwnerCprAsync(string cpr)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(Uri + $"/vehicles/owner?cpr={cpr}");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");

            string result = await responseMessage.Content.ReadAsStringAsync();
            
            IList<Vehicle> vehicles = JsonSerializer.Deserialize<IList<Vehicle>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return vehicles;
        }
    }
}