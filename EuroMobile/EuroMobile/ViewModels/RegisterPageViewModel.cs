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
    public class RegisterPageViewModel : ViewModelBase
    {
        //private readonly ILoginService _loginService;
        private string _password;

        private string _username;

        public ICommand NavigateToSignInPageCommand { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand RegisterCommandAsync { get; set; }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public RegisterPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigateToSignInPageCommand = new DelegateCommand(NavigateToSignInPageAsync);
            RegisterCommandAsync = new DelegateCommand(RegisterAsync);
            //_loginService = loginService;
        }

        private async void NavigateToSignInPageAsync()
        {
            await NavigationService.NavigateAsync(typeof(SignInPage).Name);
        }

        private async void RegisterAsync()
        {
            try
            {
                //var response = await _loginService.RegisterAsync(Username, Password);

                //if (!response.HandleErrorResponse("Register"))
                //    return;

                //var content = await response.Content.ReadAsStringAsync();

                //_loginService.HandleSuccessfullLogin(content);
            }
            catch (Exception ex)
            {
                //await NavigationService.NavigateAsync<ModalViewModel, UIMessage>(new UIMessage("Registration Error", ex.Message));
            }
        }
    }
}