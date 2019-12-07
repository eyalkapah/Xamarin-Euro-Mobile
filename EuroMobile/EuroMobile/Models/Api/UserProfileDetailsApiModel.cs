using Newtonsoft.Json;

namespace EuroMobile.Models.Api
{
    public class UserProfileDetailsApiModel
    {
        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}