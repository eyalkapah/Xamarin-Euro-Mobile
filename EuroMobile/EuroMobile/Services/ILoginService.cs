using EuroMobile.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EuroMobile.Services
{
    public interface ILoginService
    {
        event EventHandler<bool> LoggedInChanged;

        bool IsLoggedIn { get; }

        Task<HttpResponseMessage> GetUserProfileAsync();

        Task HandleSuccessfullRegistrationAsync(Stream content);

        Task<HttpResponseMessage> LogInAsync(string username, string password);

        Task<HttpResponseMessage> RegisterAsync(string username, string password);

        Task<UserProfile> SilentLoginInAsync();

        Task HandleSuccessfullLoginAsync(Stream stream);
    }
}