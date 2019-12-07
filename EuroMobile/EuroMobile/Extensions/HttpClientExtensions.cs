using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EuroMobile.Extensions
{
    public static class HttpClientExtensions
    {
        private static HttpClient _client = new HttpClient();

        public static async Task<HttpResponseMessage> HttpAuthenticatedClient(string url)
        {
            var token = await SecureStorage.GetAsync("token");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(url);

            return response;
        }
    }
}