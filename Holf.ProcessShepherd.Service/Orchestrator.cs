using Holf.ProcessShepherd.Service.Configuration;
using Holf.ProcessShepherd.Service.ProcessManagement;
using Microsoft.Extensions.Logging;
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
            ILoggerFactory loggerFactory,
            IShepherdConfigurationProvider shepherdConfigurationProvider,
            IUsernameService usernameService,
            IProcessManager processManager)
        {
            this.logger = loggerFactory.CreateLogger<Orchestrator>();
            this.shepherdConfigurationProvider = shepherdConfigurationProvider;
            this.usernameService = usernameService;
            this.processManager = processManager;
        }

        public async Task DoStuff()
        {
            var shepherdConfiguration = await shepherdConfigurationProvider.GetConfiguration();

            var loggedOnUsername = usernameService.GetLoggedOnUsername();
            if (!usernameService.GetShouldShepherdLoggedOnUser(shepherdConfiguration.ShepherdedUsers, loggedOnUsername))
            {
                logger.LogInformation($"Currently logged on user '{loggedOnUsername}' is not in list of users to be shepherded. No further action will be taken.");
                return;
            }

            processManager.TerminateUnpermittedServices(shepherdConfiguration);
        }
    }
}
