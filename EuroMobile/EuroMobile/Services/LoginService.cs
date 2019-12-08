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
            var token = await SecureStorage.GetAsync(Constants.Token);

            //var client = await HttpClientExtensions.HttpAuthenticatedClientAsync();

            var client = IoC.ClientFactory.CreateClient("AzureWebSites");

            client.BaseAddress = new Uri(GlobalSettings.DefaultBaseUrl);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Bearer, token);

            var response = await client.GetAsync(endpoint);

            return response;
        }

        public async Task HandleSuccessfullLoginAsync(Stream content)
        {
            var credentialsResult = await JsonSerializer.DeserializeAsync<ApiResponse<LoginResultApiModel>>(content);

            try
            {
                await SecureStorage.SetAsync(Constants.Email, credentialsResult.Response.Username);
                await SecureStorage.SetAsync(Constants.Token, credentialsResult.Response.Token);

                IsLoggedIn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
            {
                var endpoint = IoC.Configuration["LogInEndpoint"];

                var jsonContent = JsonSerializer.Serialize(new LoginCredentialsApiModel
                {
                    Username = username,
                    Password = password
                });

                var response = await HttpClientExtensions.PostDefaultHttpClient(endpoint, jsonContent.ToString());

                return response;
            }
            catch (Exception ex)
            {
            }

            return null;
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

        public async Task<UserProfile> SilentLoginInAsync()
        {
            try
            {
                var email = await SecureStorage.GetAsync(Constants.Email);
                var token = await SecureStorage.GetAsync(Constants.Token);

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
                {
                    ClearCredentials();

                    return null;
                }
                else
                {
                    var response = await GetUserProfileAsync();

                    response.EnsureSuccessStatusCode();

                    return await response.HandleSuccessfullSilentLogIn(this);
                }
            }
            catch (Exception ex)
            {
                //ClearCredentials();

                return null;
            }
        }

        private void ClearCredentials()
        {
            SecureStorage.Remove("email");
            SecureStorage.Remove("token");
        }
    }
}