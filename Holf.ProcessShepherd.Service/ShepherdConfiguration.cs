using System.Collections.Generic;

namespace Holf.ProcessShepherd.Service
{
	public class ShepherdConfiguration
    {
        public int PollIntervalMs { get; set; }
        
        public List<string> PermittedProcesses { get; set; }

        public List<string> ShepherdedUsers { get; set; }
    }

     
}
