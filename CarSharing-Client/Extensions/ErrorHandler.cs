using System.Collections.Generic;

namespace CarSharing_Client.Extensions
{
    public class ErrorHandler
    {
        private readonly Dictionary<int, string> _errorMessages;

        public ErrorHandler()
        {
            _errorMessages = new Dictionary<int, string>
            {
                {404, "Not found"},
                
            };
        }

        public string GetMessage(int errorCode)
        {
            return _errorMessages[errorCode];
        }
    }
}