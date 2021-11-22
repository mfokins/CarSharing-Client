using System.Threading.Tasks;
using Entity.ModelData;

namespace CarSharing_Client.Data
{
    public interface IUserService
    {
        public Task<Customer> ValidateCustomer(string username, string password);
    }
}