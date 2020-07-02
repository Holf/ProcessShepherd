using System;
using System.IO;
using Topshelf;

namespace Holf.ProcessShepherd.Service
{
	class Program
	{
		static void Main(string[] args)
		{
            HostFactory.Run(x => { 
                x.Service<ServiceManager>();
                x.SetServiceName("Process Shepherd");
                x.StartAutomatically();
            });
		}
	}

    public class ServiceManager : ServiceControl
    {
        private const string _logFileLocation = @"C:\temp\servicelog.txt";

        private void Log(string logMessage)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_logFileLocation));
            File.AppendAllText(_logFileLocation, DateTime.UtcNow.ToString() + " : " + logMessage + Environment.NewLine);
        }

        public bool Start(HostControl hostControl)
        {
            Log("Starting");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Log("Stopping");
            return true;
        }
    }
}
