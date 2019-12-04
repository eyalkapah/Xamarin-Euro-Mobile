using Prism.Ioc;
using Prism.Services;
using Prism.Services.Dialogs;

namespace EuroMobile
{
    public static class IoC
    {
        private static IContainerProvider _container;

        public static IPageDialogService PageDialog => _container.Resolve<IPageDialogService>();
        public static IDialogService DialogService => _container.Resolve<IDialogService>();

        public static void Initialize(IContainerProvider container)
        {
            _container = container;
        }
    }
}