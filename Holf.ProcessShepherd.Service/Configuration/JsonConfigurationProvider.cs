using Holf.ProcessShepherd.Service.DateTimeManagement;
using Microsoft.Extensions.Logging;
using System;
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
		private readonly ILogger<JsonConfigurationProvider> logger;
		private readonly IDateTimeService dateTimeService;

		DateTime configExipiry;
        ShepherdConfiguration configuration;

		public JsonConfigurationProvider(ILoggerFactory loggerFactory,
            IDateTimeService dateTimeService)
        {
            jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			this.logger = loggerFactory.CreateLogger<JsonConfigurationProvider>();
			this.dateTimeService = dateTimeService;
		}

        public async Task<ShepherdConfiguration> GetConfiguration()
        {
            if (configuration != null && configExipiry > dateTimeService.Now)
			{
                logger.LogDebug("Using cached configuration.");
                return configuration;
			}

            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(configurationUrl);
            var shepherdConfiguration =
                JsonSerializer.Deserialize<ShepherdConfiguration>(json, jsonSerializerOptions);

            configExipiry = dateTimeService.Now.AddMilliseconds(shepherdConfiguration.ConfigUpdatePollIntervalMs);

            if (configuration == null)
			{
                logger.LogInformation($"Configuration has been loaded for the first time. Cache duration is {shepherdConfiguration.ConfigUpdatePollIntervalMs}");
			}
			else
			{
                logger.LogDebug("Cached configuration has expired. Using fresh configuration.");
			}

            this.configuration = shepherdConfiguration;
            return shepherdConfiguration;
        }
    }


}
