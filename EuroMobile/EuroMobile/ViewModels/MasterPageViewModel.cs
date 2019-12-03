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
using System.Windows.Input;

namespace EuroMobile.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        private readonly ILoginService _loginService;
        private bool _isLoggedIn;
        private List<MasterPageItem> _navigationItems;
        private MasterPageItem _selectedMenuItem;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => SetProperty(ref _isLoggedIn, value);
        }

        public List<MasterPageItem> NavigationItems
        {
            get => _navigationItems;
            set => SetProperty(ref _navigationItems, value);
        }

        public DelegateCommand<string> OnNavigateCommand { get; set; }

        public MasterPageItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => SetProperty(ref _selectedMenuItem, value, () => OnSelectionChanged(value));
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
                NavigationItems = new List<MasterPageItem>
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

            SelectedMenuItem = null;
        }
    }
}