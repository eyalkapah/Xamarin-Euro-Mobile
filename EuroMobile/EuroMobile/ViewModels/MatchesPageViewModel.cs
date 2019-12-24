using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class MatchesPageViewModel : ViewModelBase
    {
        public ICommand AddMatchCommand { get; set; }

        public MatchesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            AddMatchCommand = new DelegateCommand(AddMatch);
        }

        private async void AddMatch()
        {
            await NavigationService.NavigateAsync(typeof(AddMatchPage).Name);
        }
    }
}