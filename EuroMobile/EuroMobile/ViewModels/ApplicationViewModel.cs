using EuroMobile.Models;
using EuroMobile.Services;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;

namespace EuroMobile.ViewModels
{
    public class ApplicationViewModel : BindableBase
    {
        private readonly ILoginService _loginService;

        private bool _isLoggedIn;
        private UserProfile _userProfile;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        public UserProfile UserProfile
        {
            get => _userProfile;
            set => SetProperty(ref _userProfile, value);
        }

        // C'tor
        //
        public ApplicationViewModel(ILoginService loginService)
        {
            _loginService = loginService;

            _loginService.LoggedInChanged += (s, e) => OnLoggedInChanged(e);
        }

        public async Task Initialize()
        {
            try
            {
                await _loginService.SilentLoginInAsync();

                await SetUserProfileAsync();
            }
            catch (Exception ex)
            {
            }
        }

        private void OnLoggedInChanged(bool isLoggedIn)
        {
            IsLoggedIn = isLoggedIn;
        }

        public async Task SetUserProfileAsync()
        {
            var respone = await _loginService.GetUserProfileAsync();

            UserProfile = await respone.HandleSuccessfullUserProfileAsync();
        }
    }
}