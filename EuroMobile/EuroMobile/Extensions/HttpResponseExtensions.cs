using EuroMobile.Models.Api;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EuroMobile.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task<string> GetResponseErrorMessage(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return string.Empty;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return "Unauthorized.";
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();

                var apiResponse = (ApiResponse)JsonConvert.DeserializeObject(json);

                return string.IsNullOrEmpty(apiResponse.ErrorMessage) ? apiResponse.ErrorMessage : $"Server responded with {response.StatusCode}";
            }
        }
    }
}