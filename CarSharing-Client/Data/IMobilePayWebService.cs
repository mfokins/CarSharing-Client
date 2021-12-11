using System.Threading.Tasks;

namespace CarSharing_Client.Data
{
    public interface IMobilePayWebService
    {
        Task<string> CreateNewPayment(decimal amount);
        Task<bool> ValidatePayment(string paymentId);
    }
}