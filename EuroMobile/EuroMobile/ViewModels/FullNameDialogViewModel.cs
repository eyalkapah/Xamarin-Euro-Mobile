using Prism.AppModel;
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
    public class FullNameDialogViewModel : BindableBase, IDialogAware, IAutoInitialize
    {
        public event Action<IDialogParameters> RequestClose;

        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private string _fullName;

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        public FullNameDialogViewModel()
        {
            OkCommand = new DelegateCommand(Ok);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            throw new NotImplementedException();
        }

        private void Ok()
        {
            throw new NotImplementedException();
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
        }
    }
}