using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Prism.Ioc;
using System;
using System.Net.Http;
using System.Reflection;
using Xamarin.Essentials;

namespace EuroMobile
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        internal static void Init(IContainerRegistry containerRegistry)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream("EuroMobile.appsettings.json"))
            {
                var host = new HostBuilder()
                    .ConfigureHostConfiguration(c =>
                    {
                        c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                        c.AddJsonStream(stream);
                    })
                    .ConfigureServices((c, x) => ConfigureServices(c, x))
                    .ConfigureLogging(l => l.AddConsole(abc =>
                    {
                        abc.DisableColors = true;
                    }))
                    .Build();

                ServiceProvider = host.Services;
            }
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            var world = ctx.Configuration["Hello"];

            if (ctx.HostingEnvironment.IsDevelopment())
            {
            }

            services.AddHttpClient("AzureWebSites", client =>
            {
                client.BaseAddress = new Uri(GlobalSettings.DefaultBaseUrl);
            })
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }));
        }
    }
}