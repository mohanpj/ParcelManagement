using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParcelManagement.Departments.Parcels.Services;

namespace ParcelManagement.Departments.Parcels
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();

            // Application entry point
            logger.LogDebug("Application Starting!");
            serviceProvider.GetService<ApplicationBootstrapper>().Run();
            logger.LogDebug("Done!");

            Console.ReadLine();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IParcelsRetrievalService, ParcelsRetrievalService>();
            services.AddLogging(configure => configure.AddConsole())
                    .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                    .AddTransient<ApplicationBootstrapper>();
            return services;
        }
    }
}
