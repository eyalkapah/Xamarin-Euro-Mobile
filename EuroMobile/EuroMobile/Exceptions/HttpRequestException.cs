using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile.Exceptions
{
    public class HttpRequestException : Exception
    {
        public string Error { get; }

        public HttpRequestException(string error)
        {
            Error = error;
        }
    }
}