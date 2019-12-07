using EuroMobile.Models;
using EuroMobile.Models.Api;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public static class LoginServiceExtensions
    {
        public static Task<UserProfile> HandleSuccessfullSilentLogIn(this HttpResponseMessage response)
        {
            return HandleSuccessfullUserProfileCall(response);
        }

        public static async Task<UserProfile> HandleSuccessfullUserProfileCall(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            var profileResult = JsonConvert.DeserializeObject<LoginApiResponse<UserProfileDetailsApiModel>>(json);

            if (!profileResult.IsSucceeded)
                throw new HttpRequestException(profileResult.Error);

            return new UserProfile
            {
                FirstName = profileResult.Response.FirstName,
                LastName = profileResult.Response.LastName,
                Bio = profileResult.Response.Bio
            };
        }
    }
}