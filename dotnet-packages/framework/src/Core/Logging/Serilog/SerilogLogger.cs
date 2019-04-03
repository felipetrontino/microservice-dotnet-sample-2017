using Framework.Core.Config;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using System;
using System.Diagnostics;

namespace Framework.Core.Logging.Serilog
{
    public class SerilogLogger : ISimpleLogger
    {
        private ILogger _logger = null;

        public SerilogLogger(IConfiguration config = null)
        {
            Configure(config);
        }

        public void Info(string message)
        {
            message = FormatMessage(message);
            _logger.Information(message);
        }

        public void Warn(string message)
        {
            message = FormatMessage(message);
            _logger.Warning(message);
        }

        public void Error(string message, Exception ex = null)
        {
            message = FormatMessage(message);
            _logger.Error(ex, message);
        }

        public void Debug(string message)
        {
            message = FormatMessage(message);
            _logger.Debug(message);
        }

        public void Log(LoggingType type, string message)
        {
            switch (type)
            {
                case LoggingType.Error:
                    Error(message);
                    break;

                case LoggingType.Warning:
                    Warn(message);
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

        private string FormatMessage(string message)
        {
            return $"[{Configuration.Audience.Get()}] - {message}";
        }

        private void Configure(IConfiguration config)
        {
            _logger = config == null ? GetLogger() : GetLogger(RabbitConfig.Get(config, ConnectionStringNames.Rabbit));
        }

        private static ILogger GetLogger(RabbitConfig rabbitConfig = null)
        {
            SelfLog.Enable(Console.Out);

            var configuration = new LoggerConfiguration();
            configuration.MinimumLevel.Debug();

            configuration.WriteTo.Console(LogEventLevel.Verbose, "[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

            if (rabbitConfig != null && !Debugger.IsAttached)
                configuration.WriteTo.RabbitMQ(rabbitConfig, Configuration.Debugging.Get() ? LogEventLevel.Information : LogEventLevel.Warning);

            configuration.Enrich.FromLogContext();

            configuration.MinimumLevel.Override("Microsoft", new LoggingLevelSwitch(LogEventLevel.Warning))
                         .MinimumLevel.Override("System", new LoggingLevelSwitch(LogEventLevel.Warning));

            return configuration.CreateLogger();
        }
    }
}
