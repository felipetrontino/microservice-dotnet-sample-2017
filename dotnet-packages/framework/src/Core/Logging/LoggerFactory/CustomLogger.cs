using Microsoft.Extensions.Logging;
using System;

namespace Framework.Core.Logging.LoggerFactory
{
    public class CustomLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LogHelper.Log(GetType(logLevel), $"{formatter(state, exception)}");
        }

        private static LoggingType GetType(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Information:
                    return LoggingType.Info;

                case LogLevel.Warning:
                    return LoggingType.Warning;

                case LogLevel.Error:
                case LogLevel.Critical:
                    return LoggingType.Error;

                default:
                    return LoggingType.Debug;
            }
        }
    }
}
