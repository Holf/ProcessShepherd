using System;
using System.IO;

namespace Holf.ProcessShepherd.Service
{
	public class Logger : ILogger
    {
        private const string _logFileLocation = @"c:/temp/ProcessShepherd.log";

        public Logger()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_logFileLocation));
        }

        public void Log(string logMessage)
        {
            File.AppendAllText(_logFileLocation, DateTime.UtcNow.ToString() + " : " + logMessage + Environment.NewLine);
        }
    }
}
