using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Holf.ProcessShepherd.Service
{
	public class JsonConfigurationProvider : IShepherdConfigurationProvider
    {
        readonly string configurationUrl = "http://localhost:8080/testConfiguration.json";
        
        public async Task<ShepherdConfiguration> GetConfiguration()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync("http://localhost:8080/testConfiguration.json");
            var shepherdConfiguration =
                JsonSerializer.Deserialize<ShepherdConfiguration>(json);

            return shepherdConfiguration;
        }
    }


}
