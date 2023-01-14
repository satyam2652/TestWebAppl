using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Test.WebApplication.Hellpers;
using Test.WebApplication.Models;

namespace Test.WebApplication.Common
{
    public class HttpRequest
    {
    }
    public class CallEndpoint
    {
        private readonly HttpClient _httpClient;
        public string _domainUri;
        public CallEndpoint(string domainUri, HttpClient httpClient = null)
        {
            _httpClient = ConfigureHttpClient(domainUri, httpClient);
        }
        private HttpClient ConfigureHttpClient(string BaseAddress, HttpClient httpClient)
        {
            if (httpClient == null)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseAddress.Trim());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _domainUri = BaseAddress;
                return client;
            }
            else
            {
                return httpClient;
            }
        }
        public async Task<TResponse> CallEndpointAsync<TResponse, TRequest>(string endPointName, HttpMethod method, TRequest requestData, RequestDataType requestDataType, string mediaType = null)
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                var requestBuilder = new RequestBuilder();
                endPointName = _domainUri + endPointName;
                var request = await requestBuilder.GenerateHttpRequest(endPointName, method, requestData, requestDataType);

                var response = _httpClient.SendAsync(request);
                httpResponseMessage = response.Result;

                if (httpResponseMessage.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var respStr = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TResponse>(respStr);
                }
                else
                    throw new Exception(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw new Exception(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
