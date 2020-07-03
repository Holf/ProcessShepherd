using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace Holf.ProcessShepherd.Service
{
    public class ServiceManager : ServiceControl
    {
        private const string _logFileLocation = @"C:\temp\servicelog.txt";
        // private readonly Timer _timer;
        private readonly IShepherdConfigurationProvider shepherdConfigurationProvider;

        public ServiceManager(IShepherdConfigurationProvider shepherdConfigurationProvider)
        {
            this.shepherdConfigurationProvider = shepherdConfigurationProvider;
        }

        private void Log(string logMessage)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_logFileLocation));
            File.AppendAllText(_logFileLocation, DateTime.UtcNow.ToString() + " : " + logMessage + Environment.NewLine);
        }

        public bool Start(HostControl hostControl)
        {
            return StartAsync().GetAwaiter().GetResult();
        }

        public async Task<bool> StartAsync()
        {
            Log("Starting");

            var config = await shepherdConfigurationProvider.GetConfiguration();

            Log($"Configuration is refreshed every {config.PollIntervalMs} milliseconds.");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Log("Stopping");
            return true;
        }
    }
}
