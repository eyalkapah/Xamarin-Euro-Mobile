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
        private string _password;
        private string _username;

        public ICommand NavigateToSignInPageCommand { get; set; }
        public ICommand RegisterCommandAsync { get; set; }


        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public RegisterPageViewModel(INavigationService navigationService, ILoginService loginService, IPageDialogService pageDialogService) : base(navigationService)
        {
            NavigateToSignInPageCommand = new DelegateCommand(NavigateToSignInPageAsync);
            RegisterCommandAsync = new DelegateCommand(RegisterAsync);
            _loginService = loginService;
            _pageDialogService = pageDialogService;
        }

        private async void NavigateToSignInPageAsync()
        {
            await NavigationService.NavigateAsync(typeof(SignInPage).Name);
        }

        private async void RegisterAsync()
        {
            try
            {
                var response = await _loginService.RegisterAsync(Username, Password);

                var errorMessage = response.GetResponseErrorMessage();

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    await _pageDialogService.DisplayAlertAsync("Registration", errorMessage, "OK");
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