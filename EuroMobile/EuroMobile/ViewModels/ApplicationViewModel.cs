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

        private UserProfile _userProfile;

        public UserProfile UserProfile
        {
            get => _userProfile;
            set => SetProperty(ref _userProfile, value);
        }

        public ApplicationViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task Initialize()
        {
            await PerformSilentLoginAsync();
        }

        private async Task PerformSilentLoginAsync()
        {
            try
            {
                var userProfile = await _loginService.SilentLoginInAsync();

                if (userProfile == null)
                {
                }

                UserProfile = userProfile;
            }
            catch (Exception ex)
            {
            }
        }
    }
}