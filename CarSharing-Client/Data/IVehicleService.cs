using System.Collections.Generic;
using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data
{
    public interface IVehicleService
    {
        Task<IList<Vehicle>> GetAllVehiclesAsync(); 
        Task AddVehicleAsync(Vehicle adult);
        Task RemoveVehicleAsync(string licenseNo);
        Task UpdateVehicleAsync(Vehicle adult);
        
        Task<Vehicle> GetVehicleAsync(string licenseNo);
    }
}