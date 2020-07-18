using Holf.ProcessShepherd.Service.Configuration;
using Holf.ProcessShepherd.Service.ProcessManagement;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace Holf.ProcessShepherd.Service
{
    public class WindowsServiceWrapper : ServiceControl
    {
        private static readonly object lockObject = new object();

        private bool processing;
        
        private readonly Microsoft.Extensions.Logging.ILogger logger;
        private readonly IOrchestrator orchestrator;
        private readonly IShepherdConfigurationProvider shepherdConfigurationProvider;
        private readonly IUsernameService usernameService;
        private readonly IProcessManager processManager;

        public WindowsServiceWrapper(
            ILoggerFactory loggerFactory,
            IOrchestrator orchestrator,
            IShepherdConfigurationProvider shepherdConfigurationProvider,
            IUsernameService usernameService,
            IProcessManager processManager)
        {
            this.logger = loggerFactory.CreateLogger<WindowsServiceWrapper>();
            this.orchestrator = orchestrator;
            this.shepherdConfigurationProvider = shepherdConfigurationProvider;
            this.usernameService = usernameService;
            this.processManager = processManager;
        }

        public bool Start(HostControl hostControl)
        {
            return StartAsync().GetAwaiter().GetResult();
        }

        public async Task<bool> StartAsync()
        {
            logger.LogInformation("Starting");

            var config = await shepherdConfigurationProvider.GetConfiguration();

            var timer = new Timer
            {
                Interval = config.PrcoessPollIntervalMs,
                AutoReset = true
            };

            timer.Elapsed += (sender, e) => Timer_Elapsed(sender, e);
            timer.Start();

            return true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (lockObject)
            {
                if (processing)
                {
                    logger.LogWarning("Attempt to start verifying processes before last verification has finished. Aborting...");
                    return;
                }

                processing = true;
            }

            orchestrator.DoStuff();

            lock (lockObject)
            {
                processing = false;
            }
        }

        public bool Stop(HostControl hostControl)
        {
            logger.LogInformation("Stopping");
            return true;
        }

        
    }
}
