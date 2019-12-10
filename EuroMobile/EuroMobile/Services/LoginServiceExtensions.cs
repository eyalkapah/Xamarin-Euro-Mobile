using Euro.Shared.In;
using Euro.Shared.Out;
using EuroMobile.Extensions;
using EuroMobile.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EuroMobile.Services
{
    public static class LoginServiceExtensions
    {
        public static async Task HandleSuccessfullLoginAsync(this HttpResponseMessage response, ILoginService loginService)
        {
            try
            {
                var stream = await response.GetContentStreamAsync(); ;

                var result = await JsonSerializer.DeserializeAsync<ApiResponse<LoginResultApiModel>>(stream);

                await SecureStorage.SetAsync(Constants.Email, result.Response.Username);
                await SecureStorage.SetAsync(Constants.Token, result.Response.Token);

                loginService.IsLoggedIn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<UserProfile> HandleSuccessfullUserProfileAsync(this HttpResponseMessage response)
        {
            var stream = await response.GetContentStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<GeneralApiResponse<UserProfileDetailsResultApiModel>>(stream);

            if (!result.IsSucceeded)
                throw new HttpRequestException(result.Error);

            return new UserProfile
            {
                FirstName = result.Response.FirstName,
                LastName = result.Response.LastName,
                Bio = result.Response.Bio,
                Email = result.Response.Email
            };
        }

        public static async Task HandleSuccessfullRegistrationAsync(this HttpResponseMessage response, ILoginService loginService)
        {
            var stream = await response.GetContentStreamAsync();

            var credentialsResult = await JsonSerializer.DeserializeAsync<ApiResponse<RegisterCredentialsResultApiModel>>(stream);

            try
            {
                await SecureStorage.SetAsync(Constants.Email, credentialsResult.Response.Email);
                await SecureStorage.SetAsync(Constants.Token, credentialsResult.Response.Token);

                loginService.IsLoggedIn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}