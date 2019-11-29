using Newtonsoft.Json;
using System.Collections.Generic;

namespace EuroMobile.Models.Api
{
    public class ApiResponse
    {
        [JsonProperty("errors")]
        public List<ErrorApiModel> Errors { get; set; }

        [JsonProperty("is_succeeded")]
        public bool IsSucceeded { get; set; }

        [JsonProperty("response")]
        public RegisterCredentialsResultApiModel Response { get; set; }
    }
}