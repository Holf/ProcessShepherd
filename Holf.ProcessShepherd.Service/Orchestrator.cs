using Holf.ProcessShepherd.Service.Configuration;
using Holf.ProcessShepherd.Service.ProcessManagement;
using System.Threading.Tasks;

namespace Holf.ProcessShepherd.Service
{
    public interface IOrchestrator
    {
        Task DoStuff();
    }

    public class Orchestrator : IOrchestrator
    {
        private readonly ILogger logger;
        private readonly IShepherdConfigurationProvider shepherdConfigurationProvider;
        private readonly IUsernameService usernameService;
        private readonly IProcessManager processManager;

        public Orchestrator(
            ILogger logger,
            IShepherdConfigurationProvider shepherdConfigurationProvider,
            IUsernameService usernameService,
            IProcessManager processManager)
        {
            this.logger = logger;
            this.shepherdConfigurationProvider = shepherdConfigurationProvider;
            this.usernameService = usernameService;
            this.processManager = processManager;
        }

        public async Task DoStuff()
        {
            var shepherdConfiguration = await shepherdConfigurationProvider.GetConfiguration();

            logger.Log($"Configuration is refreshed every {shepherdConfiguration.ConfigUpdatePollIntervalMs} milliseconds.");

            var loggedOnUsername = usernameService.GetLoggedOnUsername();
            if (!usernameService.GetShouldShepherdLoggedOnUser(shepherdConfiguration.ShepherdedUsers, loggedOnUsername))
            {
                logger.Log($"Currently logged on user '{loggedOnUsername}' is not in list of users to be shepherded. No further action will be taken.");
                return;
            }

            processManager.TerminateUnpermittedServices(shepherdConfiguration);
        }
    }
}
