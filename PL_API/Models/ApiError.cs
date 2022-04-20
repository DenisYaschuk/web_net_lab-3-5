using System.Net;

namespace PL_API.Models
{
    public class ApiError
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public ApiError(string message, HttpStatusCode code)
        {
            Status = code.ToString();
            Message = message;
        }
    }
}