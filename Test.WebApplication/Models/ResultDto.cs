using System.Net;

namespace Test.WebApplication.Models
{
    public class ResultDto
    {
        public bool Result { get; set; }
        public string ResultMessage { get; set; }
        public dynamic Details { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Token { get; set; }
        public bool IsDeviceMaxLimitReached { get; set; }
    }
}
