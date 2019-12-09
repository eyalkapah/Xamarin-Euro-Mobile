using Euro.Shared.In;
using Euro.Shared.Out;
using EuroMobile.Exceptions;
using EuroMobile.Extensions;
using EuroMobile.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EuroMobile.Services
{
    public class LoginService : ILoginService
    {
        private readonly ISettingsService _settings;

        private bool _isLoggedIn;

        public event EventHandler<bool> LoggedInChanged = delegate { };

        public event EventHandler<UserProfile> UserProfileChanged = delegate { };

        public IHttpClientFactory ClientFactory => IoC.ClientFactory;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;

                    LoggedInChanged(this, value);
                }
            }
        }

        // C'tor
        //
        public LoginService(ISettingsService settings)
        {
            _settings = settings;
        }

        public async Task<HttpResponseMessage> GetUserProfileAsync()
        {
            var endpoint = IoC.Configuration["UserProfileEndpoint"];

            var client = await HttpClientExtensions.HttpAuthenticatedClientAsync();

            var response = await client.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task HandleSuccessfullRegistrationAsync(Stream content)
        {
            var credentialsResult = await JsonSerializer.DeserializeAsync<ApiResponse<RegisterCredentialsResultApiModel>>(content);

            try
            {
                await SecureStorage.SetAsync(Constants.Email, credentialsResult.Response.Email);
                await SecureStorage.SetAsync(Constants.Token, credentialsResult.Response.Token);

                IsLoggedIn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> LogInAsync(string username, string password)
        {
            var endpoint = IoC.Configuration["LogInEndpoint"];

            var jsonContent = JsonSerializer.Serialize(new LoginCredentialsApiModel
            {
                Username = username,
                Password = password
            });

            var response = await HttpClientExtensions.PostDefaultHttpClient(endpoint, jsonContent.ToString());

            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> RegisterAsync(string username, string password)
        {
            try
            {
                var endpoint = IoC.Configuration["RegisterEndpoint"];

                var jsonContent = JsonSerializer.Serialize(new RegisterCredentialsApiModel
                {
                    Email = username,
                    Password = password
                });

                var response = await HttpClientExtensions.PostDefaultHttpClient(endpoint, jsonContent.ToString());
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public async Task SilentLoginInAsync()
        {
            try
            {
                var endpoint = IoC.Configuration["AuthEndpoint"];

                var client = await HttpClientExtensions.HttpAuthenticatedClientAsync();

                var response = await client.GetAsync(endpoint);

                response.EnsureSuccessStatusCode();

                IsLoggedIn = true;
            }
            catch (Exception ex)
            {
                IsLoggedIn = false;

                throw ex;
            }
        }

        public void Logout()
        {
            ClearCredentials();
        }

        private void ClearCredentials()
        {
            SecureStorage.Remove("email");
            SecureStorage.Remove("token");
        }
    }
}