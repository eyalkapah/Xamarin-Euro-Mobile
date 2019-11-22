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
        public ICommand LoginWithFacebookCommand { get; set; }
        public ICommand LoginWithGoogleCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public SignInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            LoginWithFacebookCommand = new DelegateCommand(LoginWithFacebook);
            LoginWithGoogleCommand = new DelegateCommand(LoginWithGoogle);
            RegisterCommand = new DelegateCommand(Register);
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