using EuroMobile.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public interface ILoginService
    {
        event EventHandler<bool> LoggedInChanged;

        bool IsLoggedIn { get; }

        Task<HttpResponseMessage> GetUserProfileAsync();

        Task HandleSuccessfullLoginAsync(string content);

        Task HandleSuccessfullRegistrationAsync(string content);

        Task<HttpResponseMessage> LogInAsync(string username, string password);

        Task<HttpResponseMessage> RegisterAsync(string username, string password);

        Task<UserProfile> SilentLoginInAsync();
    }
}