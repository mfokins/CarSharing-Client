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
        private string uri = "https://localhost:5002";
        private readonly HttpClient client;

        public VehicleWebService()
        {
            client = new HttpClient();
        }
        
        public async Task<IList<Vehicle>> GetAllVehiclesAsync()
        {
            Task<string> stringAsync = client.GetStringAsync(uri + "/Vehicles");
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
            await client.PostAsync(uri + "/Vehicles", content);
        }

        public async Task RemoveVehicleAsync(string licenseNo)
        {
            await client.DeleteAsync($"{uri}/Vehicle/{licenseNo}");
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            string vehicleAsJson = JsonSerializer.Serialize(vehicle);
            HttpContent content = new StringContent(vehicleAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PatchAsync($"{uri}/Vehicles/{vehicle.LicenseNo}", content);        }

        public async Task<Vehicle> GetVehicleAsync(string licenseNo)
        {
            Task<string> stringAsync = client.GetStringAsync($"{uri}/Vehicles/{licenseNo}");
            string message = await stringAsync;
            Vehicle result = JsonSerializer.Deserialize<Vehicle>(message);
            return result;
        }
    }
}