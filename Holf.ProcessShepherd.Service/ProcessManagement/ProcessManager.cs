using Holf.ProcessShepherd.Service.Configuration;
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

        public ProcessManager(ILogger logger, IUsernameService usernameService)
        {
            this.logger = logger;
            this.usernameService = usernameService;
        }

        public void TerminateUnpermittedServices(ShepherdConfiguration shepherdConfiguration)
        {
            var processes = Process.GetProcesses();
            var shepherdProcess = processes.SingleOrDefault(x => x.ProcessName == "ProcessShepherd");

            var users = processes.Where(x => x.ProcessName == "explorer").Select(y => y.GetProcessUser());

            if (shepherdProcess == null)
            {
                logger.Log("Oh dear...");
            }
        }

        
    }
}
