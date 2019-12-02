using Newtonsoft.Json;

namespace EuroMobile.Models.Api
{
    public class LoginApiResponse<T>
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("is_succeeded")]
        public bool IsSucceeded => Error == null;

        [JsonProperty("response")]
        public T Response { get; set; }
    }
}