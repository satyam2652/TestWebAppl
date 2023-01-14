using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.WebApplication.Models;

namespace Test.WebApplication.Hellpers
{
    public class RequestBuilder
    {
        public string _mediaType;
        public RequestBuilder()
        {
            _mediaType = "application/json";
        }
        public virtual async Task<HttpRequestMessage> GenerateHttpRequest(string endPointName, HttpMethod requestType, object requestData, RequestDataType requestDataType)
        {
            HttpRequestMessage request = null;
            var dataContent = string.Empty;
            if (requestDataType == RequestDataType.NonQueryString)
            {
                request = new HttpRequestMessage(requestType, endPointName);
            }
            else
            {
                var queryString = this.GetQueryString(requestData);
                request = new HttpRequestMessage(requestType, endPointName + queryString);
            }
            if (requestData != null && requestDataType == RequestDataType.NonQueryString)
            {
                dataContent = JsonConvert.SerializeObject(requestData);
                request.Content = new StringContent(dataContent, Encoding.UTF8, _mediaType);
            }
            return request;
        }
        private string GetQueryString(object obj)// Generates Query string parameter list
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + System.Web.HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());
            var querystring = "?" + string.Join("&", properties.ToArray());
            return querystring;
        }
    }
   
}
