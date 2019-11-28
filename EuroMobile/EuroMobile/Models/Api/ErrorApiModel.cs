using Newtonsoft.Json;

namespace EuroMobile.Models.Api
{
    public class ErrorApiModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}