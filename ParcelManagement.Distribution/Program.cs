using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParcelManagement.Distribution.Handlers;
using ParcelManagement.Infrastructure.Repositories;
using ParcelManagement.Queue.Services;

namespace ParcelManagement.Distribution
{
    public class Program
    {
        public static void Main(string[] args)
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
            services.AddTransient<IContainerRepository, ContainerRepository>();
            services.AddTransient<IDistributeParcelsHandler, DistributeParcelsHandler>();
            services.AddTransient<IPublishService, PublishService>();
            services.AddLogging(configure => configure.AddConsole())
                    .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                    .AddTransient<ApplicationBootstrapper>();
            return services;
        }
    }
}
