﻿using EuroMobile.Converters;
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
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
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

        public SignInPageViewModel(INavigationService navigationService, ILoginService loginService, IPageDialogService dialogService) : base(navigationService)
        {
            _loginService = loginService;
            _dialogService = dialogService;
            LoginCommand = new DelegateCommand(LoginAsync, () => !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Username))
                .ObservesProperty(() => Password)
                .ObservesProperty(() => Username);

            LoginWithFacebookCommand = new DelegateCommand(LoginWithFacebook);
            LoginWithGoogleCommand = new DelegateCommand(LoginWithGoogle);
            RegisterCommand = new DelegateCommand(Register);
        }

        private async void LoginAsync()
        {
            try
            {
                IsLoading = true;

                var response = await _loginService.LogInAsync(Username, Password);

                await response.HandleSuccessfullLoginAsync(_loginService);

                await ApplicationViewModel.SetUserProfileAsync();

                await NavigationService.NavigateAsync(NavigationConstants.Home);
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
            finally
            {
                IsLoading = false;
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