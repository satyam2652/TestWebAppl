using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Test.WebApplication.Common;
using Test.WebApplication.Models;

namespace Test.WebApplication.Services
{
    public class DepartmentService : IDepartmentService
    {
        private IConfiguration _configuration;
        private readonly string _baseUri;
        public DepartmentService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUri = _configuration.GetSection("BaseServiceUri").Value;
        }
        
        public async Task<DepartmentMst> GetDepartmentByID(int DepartmentId)
        {
            try
            {
                var endpoint = "api/v1/GetDepartmentDetails";

                var mediatype = "application/json";

                var ID = new { ID = DepartmentId };

                CallEndpoint _callEndpointHandler = new CallEndpoint(_baseUri);

                var responseData = await _callEndpointHandler.CallEndpointAsync<ResultDto, object>(endpoint, HttpMethod.Get, ID, RequestDataType.QueryString, mediatype);

                var responseObj = new DepartmentMst();

                if (responseData.Result == true)
                {
                    responseObj = JsonConvert.DeserializeObject<DepartmentMst>(JsonConvert.SerializeObject(responseData.Details));
                }
                return responseObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IList<DepartmentMst>> GetAllDepartments()
        {
            try
            {
                var endpoint = "api/v1/getAllDepartments";

                var mediatype = "application/json";

                CallEndpoint _callEndpointHandler = new CallEndpoint(_baseUri);

                var responseData = await _callEndpointHandler.CallEndpointAsync<ResultDto, object>(endpoint, HttpMethod.Get, "", RequestDataType.NonQueryString, mediatype);

                var responseObj = new List<DepartmentMst>();

                if (responseData.Result == true)
                {
                    responseObj = JsonConvert.DeserializeObject<IList<DepartmentMst>>(JsonConvert.SerializeObject(responseData.Details));
                }

                return responseObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OutputDto> CreateNewDepartment(DepartmentMst RequestObj)
        {
            try
            {
                var endpoint = "api/v1/CreateDepartment";

                var mediatype = "application/json";

                CallEndpoint _callEndpointHandler = new CallEndpoint(_baseUri);

                var DepartmentAdd = new DepartmentMst
                {
                    Title = RequestObj.Title,
                    CreatedDate = RequestObj.CreatedDate,
                    CreatedBy = RequestObj.CreatedBy,
                    ModifiedDate = RequestObj.ModifiedDate,
                    ModifiedBy = RequestObj.ModifiedBy
                };

                var responseData = await _callEndpointHandler.CallEndpointAsync<ResultDto, DepartmentMst>(endpoint, HttpMethod.Post, DepartmentAdd, RequestDataType.NonQueryString, mediatype);

                var result = new OutputDto();

                if (responseData.Result == true)
                {
                    result = JsonConvert.DeserializeObject<OutputDto>(JsonConvert.SerializeObject(responseData.Details));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OutputDto> UpdateDepartment(DepartmentMst RequestObj)
        {
            try
            {
                var endpoint = "api/v1/UpdateDepartment";

                var mediatype = "application/json";

                CallEndpoint _callEndpointHandler = new CallEndpoint(_baseUri);

                var responseData = await _callEndpointHandler.CallEndpointAsync<ResultDto, DepartmentMst>(endpoint, HttpMethod.Post, RequestObj, RequestDataType.NonQueryString, mediatype);

                var result = new OutputDto();

                if (responseData.Result == true)
                {
                    result = JsonConvert.DeserializeObject<OutputDto>(JsonConvert.SerializeObject(responseData.Details));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
