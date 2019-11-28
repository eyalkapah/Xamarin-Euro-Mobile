using EuroMobile.Extensions;
using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly ILoginService _loginService;
        private readonly IPageDialogService _pageDialogService;
        private string _emailErrorMessage;
        private string _password;
        private string _passwordErrorMessage;
        private string _username;

        public string EmailErrorMessage
        {
            get => _emailErrorMessage;
            set => SetProperty(ref _emailErrorMessage, value);
        }

        public ICommand NavigateToSignInPageCommand { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, () => PasswordErrorMessage = string.Empty);
        }

        public string PasswordErrorMessage
        {
            get => _passwordErrorMessage;
            set => SetProperty(ref _passwordErrorMessage, value);
        }

        public ICommand RegisterCommandAsync { get; set; }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value, () => EmailErrorMessage = string.Empty);
        }

        public RegisterPageViewModel(INavigationService navigationService, ILoginService loginService, IPageDialogService pageDialogService) : base(navigationService)
        {
            NavigateToSignInPageCommand = new DelegateCommand(NavigateToSignInPageAsync);
            RegisterCommandAsync = new DelegateCommand(RegisterAsync, () => !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Username)).ObservesProperty(() => Password).ObservesProperty(() => Username);
            _loginService = loginService;
            _pageDialogService = pageDialogService;
        }

        public void ClearErrors()
        {
            EmailErrorMessage = string.Empty;
            PasswordErrorMessage = string.Empty;
        }

        private async void NavigateToSignInPageAsync()
        {
            await NavigationService.NavigateAsync(typeof(SignInPage).Name);
        }

        private async void RegisterAsync()
        {
            try
            {
                ClearErrors();

                var response = await _loginService.RegisterAsync(Username, Password);

                if (!response.IsSuccessStatusCode)
                {
                    var errors = await response.GetResponseErrorMessage();

                    EmailErrorMessage = errors.FirstOrDefault(e => e.Code.Equals("Email"))?.Description;
                    PasswordErrorMessage = errors.FirstOrDefault(e => e.Code.Equals("Password"))?.Description;

                    //await _pageDialogService.DisplayAlertAsync("Registration", errorMessage, "OK");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();

                    _loginService.HandleSuccessfullLogin(content);

                    await NavigationService.NavigateAsync(typeof(HomePage).Name);
                }
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Registration", ex.Message, "OK");
            }
        }
    }
}