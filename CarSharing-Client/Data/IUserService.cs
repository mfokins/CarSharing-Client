using System.Threading.Tasks;
using CarSharing_Client.Models;

namespace CarSharing_Client.Data
{
    public interface IUserService
    {
        public Task<Customer> ValidateCustomer(string username, string password);
        public Task RegisterCustomer(Account account);
    }
}