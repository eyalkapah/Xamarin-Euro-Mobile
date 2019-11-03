using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EuroMobile.ViewModels
{
    public class CustomMasterDetailPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public DelegateCommand<string> OnNavigateCommand { get; set; }

        public CustomMasterDetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);
        }

        private async void NavigateAsync(string page)
        {
            await _navigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }
    }
}