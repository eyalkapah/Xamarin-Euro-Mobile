using EuroMobile.ViewModels.Base;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EuroMobile.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public DelegateCommand<string> OnNavigateCommand { get; set; }

        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);

            Title = "Home";
        }

        private async void NavigateAsync(string page)
        {
            await NavigationService.NavigateAsync(new Uri($"{page}", UriKind.Relative));
        }
    }
}