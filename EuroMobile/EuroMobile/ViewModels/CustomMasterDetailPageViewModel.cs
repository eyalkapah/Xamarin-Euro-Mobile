using EuroMobile.Models.UI;
using EuroMobile.Utils;
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

        private MasterPageItem _selectedMenuItem;

        public MasterPageItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => SetProperty(ref _selectedMenuItem, value, () => OnSelectionChanged(value));
        }

        private async void OnSelectionChanged(MasterPageItem value)
        {
            await NavigationService.NavigateAsync(value.TargetType.Name);
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
                    Glyph = FontAwesomeIcons.Home,
                    TargetType = typeof(HomePage)
                },
                new MasterPageItem
                {
                    Title = "Standings",
                    Glyph = FontAwesomeIcons.Futbol,
                    TargetType = typeof(StandingsPage)
                },
                new MasterPageItem
                {
                    Title = "Matches",
                    Glyph = FontAwesomeIcons.PollH,
                    TargetType = typeof(MatchesPage)
                }
            };
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            SelectedMenuItem = MenuItems.First();
        }

        private async void NavigateAsync(string page)
        {
            await NavigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }
    }
}