using Euro.Shared;
using EuroMobile.ViewModels.Base;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using Xamarin.Essentials;

namespace EuroMobile.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public DelegateCommand<string> OnNavigateCommand { get; set; }

        public ICommand TestCommand { get; set; }

        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            OnNavigateCommand = new DelegateCommand<string>(NavigateAsync);

            TestCommand = new DelegateCommand(TestMe);

            Title = "Home";
        }

        private async void TestMe()
        {
            //using (var client = new HttpClient())
            //{
            //    var url = $"{GlobalSettings.Instance.BaseEndpoint}/api/test";

            //    var jsonContent = JsonSerializer.Serialize(new Test
            //    {
            //        FirstName = "Eyal",
            //        LastName = "Kapah"
            //    });

            //    var response = await client.PostAsync(url,
            //        new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json"));
            //}

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Debug.Assert(false, "Not supported");
                return;
            }
            else
            {
                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                {
                    return;
                }
            }

            var token = await SecureStorage.GetAsync("token");

            var client = IoC.ClientFactory.CreateClient("AzureWebSites");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("api/profile");

            response.EnsureSuccessStatusCode();

            //return await response.HandleSuccessfullSilentLogIn();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        private async void NavigateAsync(string page)
        {
            await NavigationService.NavigateAsync(new Uri($"{page}", UriKind.Relative));
        }
    }
}