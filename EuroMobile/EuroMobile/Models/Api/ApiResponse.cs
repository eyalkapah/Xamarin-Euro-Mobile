using Newtonsoft.Json;
using System.Collections.Generic;

namespace EuroMobile.Models.Api
{
    public class ApiResponse<T>
    {
        [JsonProperty("errors")]
        public List<ErrorApiModel> Errors { get; set; }

        [JsonProperty("is_succeeded")]
        public bool IsSucceeded => Errors == null;

        [JsonProperty("response")]
        public T Response { get; internal set; }
    }
}