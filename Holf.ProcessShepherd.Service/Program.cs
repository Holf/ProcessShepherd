using Microsoft.Extensions.DependencyInjection;
using Topshelf;

namespace Holf.ProcessShepherd.Service
{
    class Program
	{
		static void Main(string[] args)
		{
            var services = new ServiceCollection();

            var serviceProvider = Bootstrapper.GetServiceProvider(services);

            HostFactory.Run(x => { 
                x.Service<ServiceControl>(x =>
                {
                    x.ConstructUsing(serviceProvider.GetService<ServiceManager>);
                    x.WhenStarted((service, hostControl) => service.Start(hostControl));
                    x.WhenStopped((service, hostControl) => service.Stop(hostControl));
                });

                x.SetServiceName("Process Shepherd");
                x.StartAutomatically();
            });
		}
	}
}
