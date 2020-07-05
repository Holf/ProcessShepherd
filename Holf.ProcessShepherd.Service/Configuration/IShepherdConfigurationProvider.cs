using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holf.ProcessShepherd.Service.Configuration
{
    public interface IShepherdConfigurationProvider
    {
        public Task<ShepherdConfiguration> GetConfiguration();
    }
}
