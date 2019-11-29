using Newtonsoft.Json;

namespace EuroMobile.Models.Api
{
    public class RegisterCredentialsResultApiModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}