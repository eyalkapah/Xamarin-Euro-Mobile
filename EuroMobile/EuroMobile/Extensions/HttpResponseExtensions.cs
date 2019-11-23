using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile.Extensions
{
    public static class HttpResponseExtensions
    {
        public static string GetResponseErrorMessage(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return string.Empty;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return "Unauthorized.";
            }
            else
            {
                return $"Server responded with {response.StatusCode}";
            }
        }
    }
}