using EuroMobile.Models.Api;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EuroMobile.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<string> GetResponseError(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<LoginApiResponse<LoginResultApiModel>>(json);

            return !apiResponse.IsSucceeded ? apiResponse.Error : null;
        }

        public static async Task<List<ErrorApiModel>> GetResponseErrors(this HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new List<ErrorApiModel>
                {
                    new ErrorApiModel
                    {
                        Code = "Status Code",
                        Description = "Unauthorized."
                    }
                };
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<RegisterCredentialsResultApiModel>>(json);

                if (!apiResponse.IsSucceeded)
                {
                    return apiResponse.Errors.Any() ? apiResponse.Errors : new List<ErrorApiModel>
                    {
                        new ErrorApiModel
                        {
                            Code = response.StatusCode.ToString(),
                            Description = $"Server responded with {response.StatusCode}"
                        }
                    };
                }

                return null;
            }
        }
    }
}