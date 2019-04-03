using Framework.Core.Config;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace Framework.Core.Logging.Serilog
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration RabbitMQ(this LoggerSinkConfiguration loggerConfiguration, RabbitConfig config, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose)
        {
            return loggerConfiguration.Sink(new RabbitSink(config), restrictedToMinimumLevel);
        }
    }
}
