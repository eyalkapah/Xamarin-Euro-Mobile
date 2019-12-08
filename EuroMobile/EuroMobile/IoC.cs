using EuroMobile.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Services;
using Prism.Services.Dialogs;
using System.Net.Http;
using Xamarin.Forms.Xaml;

namespace EuroMobile
{
    public static class IoC
    {
        private static IContainerProvider _container;

        public static IPageDialogService PageDialog => _container.Resolve<IPageDialogService>();
        public static IDialogService DialogService => _container.Resolve<IDialogService>();

        public static ApplicationViewModel ApplicationViewModel => _container.Resolve<ApplicationViewModel>();

        public static IHttpClientFactory ClientFactory => (IHttpClientFactory)Startup.ServiceProvider.GetService(typeof(IHttpClientFactory));
        //public static IHttpClientFactory ClientFactory => Startup.ServiceProvider.GetService<IHttpClientFactory>();

        public static IConfiguration Configuration => Startup.ServiceProvider.GetService<IConfiguration>();

        public static ILogger Logger => Startup.ServiceProvider.GetService<ILogger>();

        public static void Initialize(IContainerProvider container)
        {
            _container = container;
        }
    }
}