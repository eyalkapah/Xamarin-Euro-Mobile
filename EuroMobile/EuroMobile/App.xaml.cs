﻿using Prism;
using Prism.Ioc;
using EuroMobile.ViewModels;
using EuroMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EuroMobile.Services;
using System;
using EuroMobile.Views.Dialogs;
using System.Threading.Tasks;
using System.Net.Http;
using EuroMobile.Configurations;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace EuroMobile
{
    public partial class App
    {
        /*
* The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
* This imposes a limitation in which the App class must have a default constructor.
* App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
*/

        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
            InitializeServices();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var result = await NavigationService.NavigateAsync("CustomMasterDetailPage/NavigationPage/HomePage");

            var applicationViewModel = Container.Resolve<ApplicationViewModel>();
            await applicationViewModel.Initialize();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterServices();

            containerRegistry.RegisterSingleton<ApplicationViewModel>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomMasterDetailPage, CustomMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<StandingsPage, StandingsPageViewModel>();
            containerRegistry.RegisterForNavigation<MatchesPage, MatchesPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();

            containerRegistry.RegisterDialog<AddFullNameDialogView, AddFullNameDialogViewModel>();

            IoC.Initialize(Container);

            Startup.Init(containerRegistry);
            containerRegistry.RegisterForNavigation<AddMatchPage, AddMatchPageViewModel>();
        }

        private void InitializeServices()
        {
            var settings = Container.Resolve<ISettingsService>();
        }
    }
}