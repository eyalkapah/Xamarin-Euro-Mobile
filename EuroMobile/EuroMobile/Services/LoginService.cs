﻿using EuroMobile.Models.Api;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EuroMobile.Services
{
    public class LoginService : ILoginService
    {
        private readonly ISettingsService _settings;

        public bool IsLoggedIn { get; set; }

        public LoginService(ISettingsService settings)
        {
            _settings = settings;
        }

        public async void HandleSuccessfullRegistration(string content)
        {
            var credentialsResult = JsonConvert.DeserializeObject<ApiResponse>(content);

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
                    var jsonContent = JsonConvert.SerializeObject(new RegisterCredentialsApi
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
    }
}