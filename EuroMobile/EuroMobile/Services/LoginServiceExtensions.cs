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
            var json = await response.GetContentStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<GeneralApiResponse<UserProfileDetailsApiModel>>(json);

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
    }
}