﻿using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class AddFullNameDialogViewModel : BindableBase, IDialogAware, IAutoInitialize
    {
        public static string ParameterFullName = "FullName";

        public event Action<IDialogParameters> RequestClose;

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private string _fullName;

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        public AddFullNameDialogViewModel()
        {
            SaveCommand = new DelegateCommand(Save, () => !string.IsNullOrWhiteSpace(FullName)).ObservesProperty(() => FullName);
            CancelCommand = new DelegateCommand(Close);
        }

        private void Close()
        {
            RequestClose(null);
        }

        private void Save()
        {
            RequestClose(new DialogParameters { { ParameterFullName, FullName } });
        }

        public bool CanCloseDialog()
        {
            return !string.IsNullOrEmpty(FullName);
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            FullName = parameters.GetValue<string>(ParameterFullName);
        }
    }
}