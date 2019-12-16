using Euro.Shared;
using Euro.Shared.In;
using EuroMobile.Extensions;
using EuroMobile.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<HttpResponseMessage> GetProfileImageAsync()
        {
            var client = await HttpClientExtensions.HttpAuthenticatedClientAsync();

            var response = await client.GetAsync(Routes.GetProfileImage);

            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> GetUserProfileAsync()
        {
            var client = await HttpClientExtensions.HttpAuthenticatedClientAsync();

            var response = await client.GetAsync(Routes.GetUserProfile);

            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> LogInAsync(string username, string password)
        {
            var jsonContent = JsonSerializer.Serialize(new LoginCredentialsApiModel
            {
                Username = username,
                Password = password
            });

            var response = await HttpClientExtensions.PostDefaultHttpClient(Routes.LogIn, jsonContent.ToString());

            response.EnsureSuccessStatusCode();

            return response;
        }

        public void Logout()
        {
            ClearCredentials();

            IsLoggedIn = false;
        }

        public async Task<HttpResponseMessage> RegisterAsync(string username, string password)
        {
            try
            {
                var jsonContent = JsonSerializer.Serialize(new RegisterCredentialsApiModel
                {
                    Email = username,
                    Password = password
                });

                var response = await HttpClientExtensions.PostDefaultHttpClient(Routes.Register, jsonContent.ToString());
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
                var client = await HttpClientExtensions.HttpAuthenticatedClientAsync();

                var response = await client.GetAsync(Routes.Auth);

                response.EnsureSuccessStatusCode();

                IsLoggedIn = true;
            }
            catch (Exception ex)
            {
                IsLoggedIn = false;

                throw ex;
            }
        }

        public async Task<HttpResponseMessage> UpdateUserProfileAsync(UserProfile userProfile)
        {
            var jsonContent = JsonSerializer.Serialize(new UserProfileDetailsApiModel
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName
            });

            var response = await HttpClientExtensions.PostAuthenticatedClientAsync(Routes.UpdateUserProfile, jsonContent);

            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> UploadProfileImageAsync(Stream stream, string filename)
        {
            HttpContent fileStreamContent = new StreamContent(stream);
            fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = filename
            };
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            //var response = await HttpClientExtensions.PostAuthenticatedClientAsync(Routes.UpdateUserProfile, jsonContent);

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                try
                {
                    var response = await HttpClientExtensions.PostAuthenticatedClientAsync(Routes.UploadImage, formData);
                    //var response = await client.PostAsync(url, formData);
                    response.EnsureSuccessStatusCode();

                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void ClearCredentials()
        {
            SecureStorage.Remove("email");
            SecureStorage.Remove("token");
        }
    }
}