using System.Collections.Generic;

namespace Holf.ProcessShepherd.Service.Configuration
{
    public enum Mode { LoggingOnly, Shepherding }

    public class ShepherdConfiguration
    {
        public Mode Mode { get; set; }

        public int ConfigUpdatePollIntervalMs { get; set; }

        public int PrcoessPollIntervalMs { get; set; }

        public List<string> PermittedProcesses { get; set; }

        public List<string> ShepherdedUsers { get; set; }
    }


}
