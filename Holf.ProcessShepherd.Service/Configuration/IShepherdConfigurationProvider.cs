using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holf.ProcessShepherd.Service.Configuration
{
    public interface IShepherdConfigurationProvider
    {
        Task<ShepherdConfiguration> GetConfiguration();
    }
}
