using System.Threading.Tasks;

namespace CarSharing_Client.Data
{
    public interface IMobilePayWebService
    {
        Task<string> CreateNewPayment();
        Task<bool> ValidatePayment();
    }
}