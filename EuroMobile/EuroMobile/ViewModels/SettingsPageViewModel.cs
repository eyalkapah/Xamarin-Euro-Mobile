using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
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
    public class SettingsPageViewModel : BindableBase
    {
        private string _fullName;

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _email;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public ICommand ShowFullNameDialogCommand { get; set; }

        public SettingsPageViewModel()
        {
            FullName = "Eyal Kapah";
            Email = "eyalk@nomail.com";

            ShowFullNameDialogCommand = new DelegateCommand(ShowFullNameDialog);
        }

        private void ShowFullNameDialog()
        {
            IoC.DialogService.ShowDialog(typeof(FullNameDialog).Name);
        }
    }
}