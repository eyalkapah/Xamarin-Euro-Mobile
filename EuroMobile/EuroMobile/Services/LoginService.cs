using EuroMobile.Models;
using EuroMobile.Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public class LoginService : ILoginService
    {
        private readonly ISettingsService _settings;

        public LoginService(ISettingsService settings)
        {
            _settings = settings;
        }

        public void HandleSuccessfullLogin(string content)
        {
            var json = JsonConvert.DeserializeObject<LoginCredentials>(content);
        }

        public async Task<HttpResponseMessage> RegisterAsync(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(new RegisterCredentialsApi
                {
                    Username = username,
                    Password = password
                });

                return await client.PostAsync(GlobalSettings.Instance.RegisterEndpoint,
                    new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json"));
            }
        }
    }
}