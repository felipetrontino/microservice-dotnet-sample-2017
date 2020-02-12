using Framework.Core.Common;
using Framework.Core.Config;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Events;

namespace Framework.Core.Logging.Serilog
{
    public class RabbitSink : ILogEventSink
    {
        private readonly RabbitMQ.RabbitClient _client;

        public RabbitSink(RabbitConfig config)
        {
            _client = new RabbitMQ.RabbitClient(config);
        }

        public void Emit(LogEvent logEvent)
        {
            var e = new RabbitMQ.LogEvent
            {
                Audience = Configuration.Audience.Get(),
                Logger = Configuration.ProjectName.Get(),
                Stage = Configuration.Stage.Get().ToString(),
                Tenant = ServiceAccessor.Instance?.GetService<ITenantAccessor>()?.Tenant,
                Timestamp = logEvent.Timestamp.DateTime,
                Level = logEvent.Level.ToString().ToUpper(),
                Message = logEvent.RenderMessage(),
                Exception = logEvent.Exception?.ToString()
            };

            _client.Publish(e);
        }
    }
}