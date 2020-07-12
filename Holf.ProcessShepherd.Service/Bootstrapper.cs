using Holf.ProcessShepherd.Service.Configuration;
using Holf.ProcessShepherd.Service.ProcessManagement;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Holf.ProcessShepherd.Service
{
    public class Bootstrapper
    {
        public static IServiceProvider GetServiceProvider(IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();
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
