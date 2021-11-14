using System;
using System.IO;
using System.Text;

namespace Outlines.Core
{
    public enum LoggingLevel
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public static class Logger
    {
        private const string LogsDirectory = "Logs/";
        private static StreamWriter Output { get; set; }

        static Logger()
        {
            if (!Directory.Exists(LogsDirectory))
            {
                Directory.CreateDirectory(LogsDirectory);
            }
            string logFileName = $"Log-{DateTime.Now.ToFileTime()}.txt";
            string logFilePath = Path.Combine(LogsDirectory, logFileName);
            Output = new StreamWriter(File.Create(logFilePath), Encoding.UTF8);
        }

        public static void Log(LoggingLevel loggingLevel, string message)
        {
            Output.WriteLine($"[{loggingLevel}] {message}");
        }
    }
}
