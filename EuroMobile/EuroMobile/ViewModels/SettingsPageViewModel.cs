using EuroMobile.Extensions;
using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using EuroMobile.Views.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private string _fullName;

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _email;
        private readonly ILoginService _loginService;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public ICommand ShowFullNameDialogCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            //    //FullName = "Eyal Kapah";
            //    Email = "eyalk@nomail.com";

            ShowFullNameDialogCommand = new DelegateCommand(ShowFullNameDialog);
            _loginService = loginService;
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            try
            {
                var response = await _loginService.GetUserProfile();

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.GetResponseError();

                    await IoC.PageDialog.DisplayAlertAsync("Fail", error, "OK");
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();

                    //await _loginService.HandleSuccessfullRegistrationAsync(result);

                    //await NavigationService.NavigateAsync("/CustomMasterDetailPage/NavigationPage/HomePage");
                }
            }
            catch (Exception ex)
            {
                await IoC.PageDialog.DisplayAlertAsync("Fail", ex.Message, "OK");
            }
        }

        private void ShowFullNameDialog()
        {
            IoC.DialogService.ShowDialog(typeof(AddFullNameDialogView).Name,
                new DialogParameters
                {
                    { AddFullNameDialogViewModel.ParameterFullName, FullName }
                },
                o => AddFullNameCallback(o));
        }

        private void AddFullNameCallback(IDialogResult obj)
        {
            if (obj == null || obj.Parameters == null)
                return;

            FullName = obj.Parameters.GetValue<string>(AddFullNameDialogViewModel.ParameterFullName);
        }
    }
}