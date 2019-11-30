using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        private ILoginService _loginService;
        private string _password;
        private string _username;
        public ICommand LoginCommand { get; set; }
        public ICommand LoginWithFacebookCommand { get; set; }
        public ICommand LoginWithGoogleCommand { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand RegisterCommand { get; set; }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public SignInPageViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            _loginService = loginService;

            LoginCommand = new DelegateCommand(LoginAsync, () => !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Username))
                .ObservesProperty(() => Password)
                .ObservesProperty(() => Username);

            LoginWithFacebookCommand = new DelegateCommand(LoginWithFacebook);
            LoginWithGoogleCommand = new DelegateCommand(LoginWithGoogle);
            RegisterCommand = new DelegateCommand(Register);
        }

        private void LoginAsync()
        {
            try
            {
                var response = await _loginService.LoginAsync(Username, Password);

                if (!response.IsSuccessStatusCode)
                {
                    var errors = await response.GetResponseErrorMessage();

                    EmailErrorMessage = errors.FirstOrDefault(e => e.Code.Equals("Email"))?.Description;
                    PasswordErrorMessage = errors.FirstOrDefault(e => e.Code.Equals("Password"))?.Description;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();

                    _loginService.HandleSuccessfullRegistration(result);

                    await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/HomePage");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoginWithFacebook()
        {
        }

        private void LoginWithGoogle()
        {
        }

        private async void Register()
        {
            await NavigationService.NavigateAsync(typeof(RegisterPage).Name);
        }
    }
}