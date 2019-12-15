using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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

        public async static Task<HttpResponseMessage> PostAuthenticatedClientAsync(string endpoint, string content)
        {
            var client = await HttpAuthenticatedClientAsync();

            return await client.PostInternal(endpoint, content);
        }

        public async static Task<HttpResponseMessage> PostAuthenticatedClientAsync(string endpoint, HttpContent content)
        {
            var client = await HttpAuthenticatedClientAsync();

            return await client.PostAsync(endpoint, content);
        }

        public async static Task<HttpResponseMessage> PostDefaultHttpClient(string endpoint, string content)
        {
            var client = DefaultHttpClient();

            return await client.PostInternal(endpoint, content);
        }

        private static HttpClient DefaultHttpClient()
        {
            var client = IoC.ClientFactory.CreateClient(Constants.DefaultHttpClient);

            client.BaseAddress = new Uri(GlobalSettings.Instance.BaseEndpoint);

            return client;
        }

        private static async Task<HttpResponseMessage> PostInternal(this HttpClient client, string endpoint, string content)
        {
            return await client.PostAsync(endpoint,
                                new StringContent(content, Encoding.UTF8, "application/json"));
        }
    }
}