using EuroMobile.Services;
using EuroMobile.ViewModels.Base;
using EuroMobile.Views;
using EuroMobile.Views.Dialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EuroMobile.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly ILoginService _loginService;

        private ImageSource _profileImage;

        public ICommand LoadImageMenuCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        public ImageSource ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }

        private string _profileImageUri;

        public string ProfileImageUri
        {
            get => _profileImageUri;
            set => SetProperty(ref _profileImageUri, value);
        }

        public ICommand ShowFullNameDialogCommand { get; set; }

        public SettingsPageViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            ShowFullNameDialogCommand = new DelegateCommand(ShowFullNameDialog);
            LogoutCommand = new DelegateCommand(Logout);
            LoadImageMenuCommand = new DelegateCommand(async () => await LoadImageMenu());

            _loginService = loginService;

            //LoadImageMenu();
        }

        private async Task GetProfileImageUri()
        {
            var response = await _loginService.GetProfileImageUri();

            var relativePath = await response.Content.ReadAsStringAsync();

            ProfileImageUri = Path.Combine(GlobalSettings.DefaultBaseUrl, relativePath);
        }

        private async Task GetProfileImageAsync()
        {
            var response = await _loginService.GetProfileImageAsync();

            var stream = await response.Content.ReadAsStreamAsync();

            ProfileImage = ImageSource.FromStream(() => stream);
        }

        private async Task AddFullNameCallbackAsync(IDialogResult obj)
        {
            if (obj == null || obj.Parameters == null || !obj.Parameters.Any())
                return;

            var fullname = obj.Parameters.GetValue<string>(AddFullNameDialogViewModel.ParameterFullName);

            var split = fullname.Trim().Split();

            ApplicationViewModel.UserProfile.FirstName = split[0];

            if (split.Length > 1)
                ApplicationViewModel.UserProfile.LastName = split[1];

            await _loginService.UpdateUserProfileAsync(ApplicationViewModel.UserProfile);
        }

        private async Task LoadImageMenu()
        {
            //var f = new MediaFile("/storage/emulated/0/Android/data/com.companyname.appname/files/Pictures/temp/me_8.jpg", null);
            //f.pho

            try
            {
                var action = await IoC.PageDialog.DisplayActionSheetAsync("Choose photo", "Cancel", null, "Take photo", "Upload from gallery");

                if (action != null && action.Equals("Upload from gallery"))
                {
                    var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Small,
                        CompressionQuality = 90
                    });

                    if (file == null)
                        return;

                    //ProfileImage = ImageSource.FromStream(() =>
                    //{
                    //    var fileStream = file.GetStream();
                    //    return fileStream;
                    //});

                    var response = await _loginService.UploadProfileImageAsync(file.GetStream(), Path.GetFileName(file.Path));

                    var profileImagePath = await response.HandleSuccessfullUploadProfileImageAsync();

                    ApplicationViewModel.SetProfileImage(profileImagePath);

                    file.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void Logout()
        {
            _loginService.Logout();

            await NavigationService.NavigateAsync($"/{typeof(CustomMasterDetailPage).Name}/{typeof(NavigationPage).Name}/{typeof(SignInPage).Name}");
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