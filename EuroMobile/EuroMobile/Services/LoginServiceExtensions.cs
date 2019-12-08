using Euro.Shared.In;
using Euro.Shared.Out;
using EuroMobile.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public static class LoginServiceExtensions
    {
        public static Task<UserProfile> HandleSuccessfullSilentLogIn(this HttpResponseMessage response, LoginService loginService)
        {
            loginService.IsLoggedIn = true;

            return HandleSuccessfullUserProfileCall(response);
        }

        public static async Task<UserProfile> HandleSuccessfullUserProfileCall(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStreamAsync();

            var profileResult = await JsonSerializer.DeserializeAsync<GeneralApiResponse<UserProfileDetailsApiModel>>(json);

            if (!profileResult.IsSucceeded)
                throw new HttpRequestException(profileResult.Error);

            return new UserProfile
            {
                FirstName = profileResult.Response.FirstName,
                LastName = profileResult.Response.LastName,
                Bio = profileResult.Response.Bio,
                Email = profileResult.Response.Email
            };
        }
    }
}