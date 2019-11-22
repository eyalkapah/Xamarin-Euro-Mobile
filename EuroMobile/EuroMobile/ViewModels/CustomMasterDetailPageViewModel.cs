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
        private List<MasterPageItem> __secondaryMenuItems;
        private List<MasterPageItem> _menuItems;
        private MasterPageItem _selectedMenuItem;
        private MasterPageItem _selectedSecondaryMenuItem;

        public List<MasterPageItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public DelegateCommand<string> OnNavigateCommand { get; set; }

        public List<MasterPageItem> SecondaryMenuItems
        {
            get => __secondaryMenuItems;
            set => SetProperty(ref __secondaryMenuItems, value);
        }

        public MasterPageItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => SetProperty(ref _selectedMenuItem, value, () => OnSelectionChanged(value));
        }

        public MasterPageItem SelectedSecondaryMenuItem
        {
            get => _selectedSecondaryMenuItem;
            set => SetProperty(ref _selectedSecondaryMenuItem, value, () => OnSelectionChanged(value));
        }

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

            SecondaryMenuItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Add account",
                    Glyph = FontAwesomeIcons.Play,
                    TargetType = typeof(SignInPage)
                },
                new MasterPageItem
                {
                    Title = "Settins",
                    Glyph = FontAwesomeIcons.Cog,
                    TargetType = typeof(SettingsPage)
                }
            };
        }

        private async void NavigateAsync(string page)
        {
            await NavigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }

        private async void OnSelectionChanged(MasterPageItem selectedItem)
        {
            if (selectedItem == null)
                return;

            await NavigationService.NavigateAsync(new Uri($"NavigationPage/{selectedItem.TargetType.Name}", UriKind.Relative));

            SelectedSecondaryMenuItem = null;
            SelectedMenuItem = null;
        }
    }
}