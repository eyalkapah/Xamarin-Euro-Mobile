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
        private readonly ILoginService _loginService;
        private string _email;
        private string _fullName;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        public ICommand ShowFullNameDialogCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            //    //FullName = "Eyal Kapah";
            //    Email = "eyalk@nomail.com";

            ShowFullNameDialogCommand = new DelegateCommand(ShowFullNameDialog);
            _loginService = loginService;
        }

        private void AddFullNameCallback(IDialogResult obj)
        {
            if (obj == null || obj.Parameters == null)
                return;

            FullName = obj.Parameters.GetValue<string>(AddFullNameDialogViewModel.ParameterFullName);
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
    }
}