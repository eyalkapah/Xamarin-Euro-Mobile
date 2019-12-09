using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EuroMobile.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpClient> HttpAuthenticatedClientAsync()
        {
            try
            {
                var token = await SecureStorage.GetAsync(Constants.Token);

                var client = IoC.ClientFactory.CreateClient("AzureWebSites");

                client.BaseAddress = new Uri(GlobalSettings.DefaultBaseUrl);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Bearer, token);

                return client;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static HttpClient DefaultHttpClient()
        {
            var client = IoC.ClientFactory.CreateClient(Constants.DefaultHttpClient);

            client.BaseAddress = new Uri(GlobalSettings.Instance.BaseEndpoint);

            return client;
        }

        public async static Task<HttpResponseMessage> PostDefaultHttpClient(string endpoint, string content)
        {
            var client = DefaultHttpClient();

            var response = await client.PostAsync(endpoint,
                    new StringContent(content, Encoding.UTF8, "application/json"));

            return response;
        }
    }
}