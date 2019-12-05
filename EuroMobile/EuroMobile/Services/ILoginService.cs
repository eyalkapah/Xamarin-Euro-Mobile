using EuroMobile.Models;
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

        UserInfo GetUserInfo();

        Task HandleSuccessfullRegistrationAsync(string content);

        Task<HttpResponseMessage> LogInAsync(string username, string password);

        Task<HttpResponseMessage> RegisterAsync(string username, string password);

        Task HandleSuccessfullLoginAsync(string content);

        Task<HttpResponseMessage> GetUserProfile();
    }
}