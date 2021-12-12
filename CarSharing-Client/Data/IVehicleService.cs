using System.Collections.Generic;
using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data
{
    public interface IVehicleService
    {

        Task AddVehicleAsync(Vehicle vehicle);
        Task RemoveVehicleAsync(string licenseNo);
        Task UpdateVehicleAsync(Vehicle vehicle);

        Task<Vehicle> GetVehicleAsync(string licenseNo);

        Task<IList<Vehicle>> GetVehiclesByOwnerCprAsync(string cpr);
        Task<IList<Vehicle>> GetVehiclesWaitingForApproval();

    }
}
    