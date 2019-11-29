using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace EuroMobile.ViewModels
{
    public class CustomMasterDetailPageViewModel : ViewModelBase
    {
        private MasterPageViewModel _masterPageViewModel;

        public MasterPageViewModel MasterPageViewModel
        {
            get => _masterPageViewModel;
            set => SetProperty(ref _masterPageViewModel, value);
        }

        public DelegateCommand<string> OnNavigateCommand { get; set; }

        public CustomMasterDetailPageViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);

            MasterPageViewModel = new MasterPageViewModel(navigationService, loginService);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            await MasterPageViewModel.BuildNavigationMenuAsync();
        }

        private async void NavigateAsync(string page)
        {
            await NavigationService.NavigateAsync(new Uri(page, UriKind.Relative));
        }
    }
}