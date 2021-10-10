using System;

namespace Outlines
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

    public interface ILogger
    {
        void Log(LoggingLevel logginLevel, string message);
    }
}
