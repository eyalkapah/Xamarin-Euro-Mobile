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

        bool IsLoggedIn { get; set; }

        Task<HttpResponseMessage> GetUserProfileAsync();

        Task<HttpResponseMessage> LogInAsync(string username, string password);

        void Logout();

        Task<HttpResponseMessage> RegisterAsync(string username, string password);

        Task SilentLoginInAsync();

        Task<HttpResponseMessage> UpdateUserProfileAsync(UserProfile userProfile);

        Task<HttpResponseMessage> UploadProfileImageAsync(Stream stream, string filename);
    }
}