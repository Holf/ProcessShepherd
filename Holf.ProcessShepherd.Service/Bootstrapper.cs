using Holf.ProcessShepherd.Service.Configuration;
using Holf.ProcessShepherd.Service.ProcessManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Holf.ProcessShepherd.Service
{
    public class Bootstrapper
    {
        public static IServiceProvider GetServiceProvider(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("LoggingConfig.json")
                .Build();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"));
                builder.AddFile(o => o.RootPath = AppContext.BaseDirectory);
            });

            services.AddSingleton<IShepherdConfigurationProvider, JsonConfigurationProvider>();
            services.AddSingleton<IUsernameService, UsernameService>();
            services.AddSingleton<ILoggedOnUsersService, LoggedOnUsersService>();
            services.AddSingleton<IProcessManager, ProcessManager>();
            services.AddSingleton<IOrchestrator, Orchestrator>();
            services.AddSingleton<WindowsServiceWrapper>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
