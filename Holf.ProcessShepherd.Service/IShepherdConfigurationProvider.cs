using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holf.ProcessShepherd.Service
{
    public interface IShepherdConfigurationProvider
    {
        public Task<ShepherdConfiguration> GetConfiguration();
    }

    public class MockConfigurationProvider : IShepherdConfigurationProvider
    {
        public Task<ShepherdConfiguration> GetConfiguration()
        {
            return Task.FromResult(new ShepherdConfiguration
            { 
                PollIntervalMs = 5000,
                PermittedProcesses = new List<string>(),
                ShepherdedUsers = new List<string>()
            });
        }
    }


}
