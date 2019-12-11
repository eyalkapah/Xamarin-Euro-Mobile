using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using EuroMobile.Views.Dialogs;
using Plugin.Media;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EuroMobile.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly ILoginService _loginService;
        private string _email;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public ICommand ShowFullNameDialogCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand LoadImageMenuCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            ShowFullNameDialogCommand = new DelegateCommand(ShowFullNameDialog);
            LogoutCommand = new DelegateCommand(Logout);
            LoadImageMenuCommand = new DelegateCommand(LoadImageMenu);

            _loginService = loginService;
        }

        private async void LoadImageMenu()
        {
            var action = await IoC.PageDialog.DisplayActionSheetAsync("Choose photo", "Cancel", null, "Take photo", "Upload from gallery");

            if (action.Equals("Upload from gallery"))
            {
                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                {
                    return;
                }
            }
        }

        private async void Logout()
        {
            _loginService.Logout();

            await NavigationService.NavigateAsync($"/{typeof(CustomMasterDetailPage).Name}/{typeof(NavigationPage).Name}/{typeof(SignInPage).Name}");
        }

        private async Task AddFullNameCallbackAsync(IDialogResult obj)
        {
            if (obj == null || obj.Parameters == null || !obj.Parameters.Any())
                return;

            var fullname = obj.Parameters.GetValue<string>(AddFullNameDialogViewModel.ParameterFullName);

            var split = fullname.Split();

            ApplicationViewModel.UserProfile.FirstName = split[0];

            if (split.Length > 1)
                ApplicationViewModel.UserProfile.LastName = split[1];

            await _loginService.UpdateUserProfileAsync(ApplicationViewModel.UserProfile);
        }

        private void ShowFullNameDialog()
        {
            IoC.DialogService.ShowDialog(typeof(AddFullNameDialogView).Name,
                new DialogParameters
                {
                    { AddFullNameDialogViewModel.ParameterFullName, ApplicationViewModel.UserProfile.FullName }
                },
                o => AddFullNameCallbackAsync(o));
        }
    }
}