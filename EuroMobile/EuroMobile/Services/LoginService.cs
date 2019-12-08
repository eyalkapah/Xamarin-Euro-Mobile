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

        public Task<HttpResponseMessage> GetUserProfileAsync()
        {
            return HttpClientExtensions.HttpAuthenticatedClient(GlobalSettings.Instance.UserProfileEndPoint);
        }

        public async Task HandleSuccessfullLoginAsync(Stream content)
        {
            var credentialsResult = await JsonSerializer.DeserializeAsync<ApiResponse<LoginResultApiModel>>(content);

            try
            {
                await SecureStorage.SetAsync("email", credentialsResult.Response.Username);
                await SecureStorage.SetAsync("token", credentialsResult.Response.Token);

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
                await SecureStorage.SetAsync("email", credentialsResult.Response.Email);
                await SecureStorage.SetAsync("token", credentialsResult.Response.Token);

                IsLoggedIn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> LogInAsync(string username, string password)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var jsonContent = JsonSerializer.Serialize(new LoginCredentialsApiModel
                    {
                        Username = username,
                        Password = password
                    });

                    return await client.PostAsync(GlobalSettings.Instance.LogInEndpoint,
                        new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json"));
                }
                catch (Exception ex)
                {
                }
            }

            return null;
        }

        public async Task<HttpResponseMessage> RegisterAsync(string username, string password)
        {
            //using (var client = new HttpClient())
            //{
            //    var result = await client.GetAsync("http://10.0.2.2:5000/api/team");
            //}
            using (var client = new HttpClient())
            {
                try
                {
                    var jsonContent = JsonSerializer.Serialize(new RegisterCredentialsApiModel
                    {
                        Email = username,
                        Password = password
                    });

                    return await client.PostAsync(GlobalSettings.Instance.RegisterEndpoint,
                        new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json"));
                }
                catch (Exception ex)
                {
                }
            }

            return null;
        }

        public async Task<UserProfile> SilentLoginInAsync()
        {
            try
            {
                var email = await SecureStorage.GetAsync("email");
                var token = await SecureStorage.GetAsync("token");

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
                {
                    ClearCredentials();

                    return null;
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var response = await client.GetAsync(GlobalSettings.Instance.UserProfileEndPoint);

                        response.EnsureSuccessStatusCode();

                        return await response.HandleSuccessfullSilentLogIn();
                    }
                }
            }
            catch (Exception ex)
            {
                ClearCredentials();

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