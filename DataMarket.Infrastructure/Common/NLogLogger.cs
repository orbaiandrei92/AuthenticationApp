using NLog;
using System;

namespace DataMarket.Infrastructure
{
    public class NLogLogger : ILogger
    {
        private Logger CurrentLogger { get; set; }

        public NLogLogger()
        {
            this.CurrentLogger = LogManager.GetCurrentClassLogger();
        }

        public void Log(string message)
        {
            Log(LoggingLevel.Info, message);
        }

        public void Log(string message, params object[] args)
        {
            Log(LoggingLevel.Info, message, args);
        }

        public void Log(LoggingLevel logLevel, string message)
        {
            var nLogLevel = ToNLogLevel(logLevel);

            if (this.CurrentLogger.IsEnabled(nLogLevel))
            {
                this.CurrentLogger.Log(nLogLevel, message);
            }
        }

        public void Log(LoggingLevel logLevel, string message, params object[] args)
        {
            var nLogLevel = ToNLogLevel(logLevel);
            if (this.CurrentLogger.IsEnabled(nLogLevel))
            {
                this.CurrentLogger.Log(nLogLevel, message, args);
            }
        }

        public void Log(LoggingLevel logLevel, string message, Exception exception)
        {
            var nLogLevel = ToNLogLevel(logLevel);

            if (this.CurrentLogger.IsEnabled(nLogLevel))
            {
                this.CurrentLogger.Log(nLogLevel, exception, message);
            }
        }

        private LogLevel ToNLogLevel(LoggingLevel logLevel)
        {
            var level = LogLevel.Off;

            switch (logLevel)
            {
                case LoggingLevel.Debug:
                    level = LogLevel.Debug;
                    break;
                case LoggingLevel.Error:
                    level = LogLevel.Error;
                    break;
                case LoggingLevel.Fatal:
                    level = LogLevel.Fatal;
                    break;
                case LoggingLevel.Info:
                    level = LogLevel.Info;
                    break;
                case LoggingLevel.Trace:
                    level = LogLevel.Trace;
                    break;
                case LoggingLevel.Warn:
                    level = LogLevel.Warn;
                    break;
            }

            return level;
        }
    }
}