using Newtonsoft.Json;

namespace EuroMobile.Models.Api
{
    public class RegisterCredentialsApi
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}