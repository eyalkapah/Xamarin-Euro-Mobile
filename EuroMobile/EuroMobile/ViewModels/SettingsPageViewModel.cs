using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public SettingsPageViewModel()
        {
            FullName = "Eyal Kapah";
            Email = "eyalk@nomail.com";
        }
    }
}