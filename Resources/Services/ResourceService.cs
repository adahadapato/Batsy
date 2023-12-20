using Batsy.Dto;
using Batsy.Models;
using Batsy.Resources.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Batsy.Resources.Services
{
    public class ResourceService : IResourceService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenContainer _tokenContainer;
        public ResourceService(HttpClient httpClient, ITokenContainer tokenContainer)
        {
            _httpClient = httpClient;
            _tokenContainer = tokenContainer;

        }

        /// <summary>
        /// login user
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message, LoginResponse? Data)> Login(LoginRequest data)
        {
            try
            {
                string strPayload = JsonConvert.SerializeObject(data);
                HttpContent _content = new StringContent(strPayload, Encoding.UTF8, "application/json");

               
                var response = await _httpClient.PostAsync("accounts/login", _content);
                if (!response.IsSuccessStatusCode) return (false, response.StatusCode.ToString(), null);
                string result = await response.Content.ReadAsStringAsync();

                if (result == null) return (false, "An error uccored trying to log in", null);

                var _response = JsonConvert.DeserializeObject<ApiResponse>(result);

                if (_response == null) return (false, "Unable to login at this time", null);
                if (!_response.IsSuccessful) return (false, _response.ErrorMessages.FirstOrDefault(), null);

                IList collection = (IList)_response.Result;

                   var _rec = JsonConvert.DeserializeObject<LoginResponse>(JsonConvert.SerializeObject(collection));

                return (true, "", _rec);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, new LoginResponse());
            }
        }

        /// <summary>
        /// Gets staff details
        /// </summary>
        /// <param name="_staffNo"></param>
        /// <returns></returns>
        public async Task<(bool Success, string Message, StaffObject? Data)> GetStaffDetails(string _staffNo)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenContainer.GetToken());
                var response = await _httpClient.GetAsync($"staff/data/number/{_staffNo}");
                if (!response.IsSuccessStatusCode) return (false, response.StatusCode.ToString(),null);
                string result = await response.Content.ReadAsStringAsync();

                if (result == null) return (false, "", null);

                var _response = JsonConvert.DeserializeObject<ApiResponse>(result);

                if (_response == null) return (false, "", null);
                if (!_response.IsSuccessful) return (false, "", null);

                IList collection = (IList)_response.Result;

                var _rec = JsonConvert.DeserializeObject<StaffObject>(JsonConvert.SerializeObject(collection));

                return (true, "", _rec);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
    }
}
