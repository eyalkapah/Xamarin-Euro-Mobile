using EuroMobile.Models.UI;
using EuroMobile.Utils;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EuroMobile.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        private List<MasterPageItem> _navigationItems;
        private MasterPageItem _selectedMenuItem;

        public List<MasterPageItem> NavigationItems
        {
            get => _navigationItems;
            set => SetProperty(ref _navigationItems, value);
        }

        public DelegateCommand<string> OnNavigateCommand { get; set; }
        public ICommand NavigateToSignInPageCommand { get; set; }

        public MasterPageItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => SetProperty(ref _selectedMenuItem, value, () => OnSelectionChanged(value));
        }

        public MasterPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            NavigateToSignInPageCommand = new DelegateCommand(NavigateToSignInPage, CanNavigateToSignInPage).ObservesProperty(() => ApplicationViewModel.IsLoggedIn);
        }

        private bool CanNavigateToSignInPage()
        {
            return !ApplicationViewModel.IsLoggedIn;
        }

        private async void NavigateToSignInPage()
        {
            await NavigationService.NavigateAsync($"{typeof(NavigationPage).Name}/{typeof(SignInPage).Name}");
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

        private async void OnSelectionChanged(MasterPageItem selectedItem)
        {
            if (selectedItem == null)
                return;

            await NavigationService.NavigateAsync(new Uri($"NavigationPage/{selectedItem.TargetType.Name}", UriKind.Relative));

            SelectedMenuItem = null;
        }
    }
}