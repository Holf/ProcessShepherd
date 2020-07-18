using Holf.ProcessShepherd.Service.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace Holf.ProcessShepherd.Service.ProcessManagement
{
    public interface IProcessManager
    {
        void TerminateUnpermittedServices(ShepherdConfiguration shepherdConfiguration);
    }

    public class ProcessManager : IProcessManager
    {
        private readonly ILogger logger;
        private readonly IUsernameService usernameService;
        private readonly ILoggedOnUsersService loggedOnUsersService;

        public ProcessManager(ILoggerFactory logger, IUsernameService usernameService, ILoggedOnUsersService loggedOnUsersService)
        {
            this.logger = logger.CreateLogger<ProcessManager>();
            this.usernameService = usernameService;
            this.loggedOnUsersService = loggedOnUsersService;
        }

        public void TerminateUnpermittedServices(ShepherdConfiguration shepherdConfiguration)
        {
            var usernamesAndSessionIds = loggedOnUsersService.GetUsernamesAndSessionIds();
            var loggedOnUsername = usernameService.GetLoggedOnUsername();
            int sessionIdForLoggedOnUser = usernamesAndSessionIds.Single(x => x.Username == loggedOnUsername).SessionId;

            var processesForLoggedOnUser = Process.GetProcesses().Where(x => x.SessionId == sessionIdForLoggedOnUser);
            var shepherdProcess = processesForLoggedOnUser.SingleOrDefault(x => x.ProcessName == "ProcessShepherd");

       
           // var userProcesses = processes.Where(x => x.SessionId == 1 && x.MainWindowTitle != string.Empty).ToList(); // processes.Where(x => x.GetProcessUser() == username).ToList();

            if (shepherdProcess != null)
            {
                logger.LogInformation("Shepherd Process is running... no processes will be terminated");
                return;
            }

            var windowedProcessesForLoggedOnUser = processesForLoggedOnUser.Where(x => x.MainWindowTitle != string.Empty);
        }
    }
}
