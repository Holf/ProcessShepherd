using Microsoft.Extensions.DependencyInjection;
using System;

namespace Holf.ProcessShepherd.Service
{
	public class Bootstrapper
    {
        public static IServiceProvider GetServiceProvider(IServiceCollection services)
        {
            services.AddSingleton<IShepherdConfigurationProvider, JsonConfigurationProvider>();
            services.AddSingleton<ServiceManager>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
