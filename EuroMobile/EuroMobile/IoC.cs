using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using Prism.Services;
using Prism.Services.Dialogs;
using System.Net.Http;

namespace EuroMobile
{
    public static class IoC
    {
        private static IContainerProvider _container;

        public static IPageDialogService PageDialog => _container.Resolve<IPageDialogService>();
        public static IDialogService DialogService => _container.Resolve<IDialogService>();

        public static IHttpClientFactory ClientFactory => (IHttpClientFactory)Startup.ServiceProvider.GetService(typeof(IHttpClientFactory));

        public static IConfiguration Configuration => (IConfiguration)Startup.ServiceProvider.GetService(typeof(IConfiguration));

        public static void Initialize(IContainerProvider container)
        {
            _container = container;
        }
    }
}