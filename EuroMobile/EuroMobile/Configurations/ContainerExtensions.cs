using EuroMobile.Services;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobile.Configurations
{
    public static class ContainerExtensions
    {
        public static void RegisterServices(this IContainerRegistry container)
        {
            container.RegisterSingleton<ILoginService, LoginService>();
            container.RegisterSingleton<ITeamService, TeamService>();
            container.RegisterSingleton<ISettingsService, SettingsService>();
            container.RegisterSingleton<IMatchService, MatchService>();
        }
    }
}