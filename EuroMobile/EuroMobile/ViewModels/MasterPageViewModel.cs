using EuroMobile.Models.UI;
using EuroMobile.Services;
using EuroMobile.Utils;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EuroMobile.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        private readonly ILoginService _loginService;
        private List<MasterPageItem> __secondaryMenuItems;
        private bool _isLoggedIn;
        private List<MasterPageItem> _menuItems;
        private MasterPageItem _selectedMenuItem;
        private MasterPageItem _selectedSecondaryMenuItem;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

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

        public MasterPageViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            _loginService = loginService;

            _loginService.LoggedInChanged += LoggedInChanged;
        }

        internal Task BuildNavigationMenuAsync()
        {
            return Task.Run(() =>
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

                SecondaryMenuItems = new List<MasterPageItem>();

                if (!_loginService.IsLoggedIn)
                    SecondaryMenuItems.Add(new MasterPageItem
                    {
                        Title = "Add account",
                        Glyph = FontAwesomeIcons.Plus,
                        TargetType = typeof(SignInPage)
                    });

                SecondaryMenuItems.Add(
                    new MasterPageItem
                    {
                        Title = "Settings",
                        Glyph = FontAwesomeIcons.Cog,
                        TargetType = typeof(SettingsPage)
                    });
            });
        }

        private void LoggedInChanged(object sender, bool isLoggedIn)
        {
            IsLoggedIn = IsLoggedIn;
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