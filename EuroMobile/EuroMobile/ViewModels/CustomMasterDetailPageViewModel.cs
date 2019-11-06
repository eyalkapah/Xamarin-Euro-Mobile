using EuroMobile.Models.UI;
using EuroMobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EuroMobile.ViewModels
{
    public class CustomMasterDetailPageViewModel : ViewModelBase
    {
        private List<MasterPageItem> _menuItems;

        public List<MasterPageItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public DelegateCommand<string> OnNavigateCommand { get; set; }

        public CustomMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);

            BuildNavigationMenu();
        }

        private void BuildNavigationMenu()
        {
            MenuItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Home",
                    Glyph = "&#xE80F;",
                    TargetType = typeof(HomePage)
                },
                new MasterPageItem
                {
                    Title = "Standings",
                    Glyph = "&#xE902;",
                    TargetType = typeof(StandingsPage)
                },
                new MasterPageItem
                {
                    Title = "Matches",
                    Glyph = "&#xE9E9;",
                    TargetType = typeof(MatchesPage)
                }
            };
        }

        private async void NavigateAsync(string page)
        {
            await NavigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }
    }
}