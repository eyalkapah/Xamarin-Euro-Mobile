using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public interface ILoginService
    {
        event EventHandler<bool> LoggedInChanged;

        bool IsLoggedIn { get; }

        void HandleSuccessfullRegistration(string content);

        Task<HttpResponseMessage> RegisterAsync(string username, string password);
    }
}