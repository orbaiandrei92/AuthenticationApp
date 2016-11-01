using System;

namespace DataMarket.Infrastructure
{
    public interface ILogger
    {
        void Log(string message);
        void Log(string message, params object[] args);
        void Log(LoggingLevel logLevel, string message);
        void Log(LoggingLevel logLevel, string message, params object[] args);
        void Log(LoggingLevel logLevel, string message, Exception exception);
    }
}
