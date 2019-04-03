using Framework.Core.Logging.Serilog;
using System;

namespace Framework.Core.Logging
{
    public static class LogHelper
    {
        private static readonly object _lock = new object();
        private static ISimpleLogger _logger = null;

        public static ISimpleLogger Logger
        {
            get
            {
                lock (_lock)
                {
                    if (_logger == null)
                        _logger = new SerilogLogger();

                    return _logger;
                }
            }
            set
            {
                lock (_lock)
                {
                    _logger = value;
                }
            }
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Warning(string message)
        {
            Logger.Warn(message);
        }

        public static void Error(Exception ex)
        {
            if (ex == null) return;

            Logger.Error(ex.Message, ex);
        }

        public static void Error(string message, Exception ex = null)
        {
            Logger.Error(message, ex);
        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }

        public static void Log(LoggingType type, string message)
        {
            switch (type)
            {
                case LoggingType.Error:
                    Error(message);
                    break;

                case LoggingType.Warning:
                    Warning(message);
                    break;

                case LoggingType.Info:
                    Info(message);
                    break;

                case LoggingType.Debug:
                    Debug(message);
                    break;

                default:
                    break;
            }
        }
    }
}
