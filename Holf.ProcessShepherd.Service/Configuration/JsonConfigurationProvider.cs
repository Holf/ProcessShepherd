using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Holf.ProcessShepherd.Service.Configuration
{
    public class JsonConfigurationProvider : IShepherdConfigurationProvider
    {
        readonly string configurationUrl = "http://localhost:8080/testConfiguration.json";
        readonly JsonSerializerOptions jsonSerializerOptions;

        public JsonConfigurationProvider()
        {
            jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task<ShepherdConfiguration> GetConfiguration()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(configurationUrl);
            var shepherdConfiguration =
                JsonSerializer.Deserialize<ShepherdConfiguration>(json, jsonSerializerOptions);

            return shepherdConfiguration;
        }
    }


}
