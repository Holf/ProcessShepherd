using Holf.ProcessShepherd.Service.Configuration;
using Holf.ProcessShepherd.Service.ProcessManagement;
using System;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace Holf.ProcessShepherd.Service
{
    public class WindowsServiceWrapper : ServiceControl
    {
        private static readonly object lockObject = new object();

        private bool processing;
        
        private readonly ILogger logger;
        private readonly IOrchestrator orchestrator;
        private readonly IShepherdConfigurationProvider shepherdConfigurationProvider;
        private readonly IUsernameService usernameService;
        private readonly IProcessManager processManager;

        public WindowsServiceWrapper(
            ILogger logger,
            IOrchestrator orchestrator,
            IShepherdConfigurationProvider shepherdConfigurationProvider,
            IUsernameService usernameService,
            IProcessManager processManager)
        {
            this.logger = logger;
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
            logger.Log("Starting");

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
                    logger.Log("Already processing");
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
            logger.Log("Stopping");
            return true;
        }

        
    }
}
