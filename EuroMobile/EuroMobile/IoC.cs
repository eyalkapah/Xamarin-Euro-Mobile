using Prism.Ioc;
using Prism.Services;

namespace EuroMobile
{
    public static class IoC
    {
        private static IContainerProvider _container;

        public static IPageDialogService PageDialog => _container.Resolve<IPageDialogService>();

        public static void Initialize(IContainerProvider container)
        {
            _container = container;
        }
    }
}