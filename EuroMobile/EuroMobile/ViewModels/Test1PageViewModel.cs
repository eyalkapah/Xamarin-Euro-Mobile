using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EuroMobile.ViewModels
{
    public class Test1PageViewModel : BindableBase, IActiveAware
    {
        private bool _isActive;
        private INavigationService _navigationService;

        public event EventHandler IsActiveChanged;

        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        public DelegateCommand OnLoginCommand { get; set; }

        public Test1PageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            OnLoginCommand = new DelegateCommand(GoHome);
        }

        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }

        private async void GoHome()
        {
            await _navigationService.NavigateAsync(new Uri($"HomePage", UriKind.Relative), null, false);
        }
    }
}