using System.Collections.Generic;

namespace EuroMobile.Models.Api
{
    public class ApiResponse
    {
        public List<ErrorApiModel> Errors { get; set; }
        public bool IsSucceeded { get; set; }
        public object Response { get; set; }
    }
}