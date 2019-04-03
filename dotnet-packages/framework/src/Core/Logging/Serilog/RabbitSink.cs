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
            var e = new RabbitMQ.LogEvent();
            e.Audience = Configuration.Audience.Get();
            e.Logger = Configuration.ProjectName.Get();
            e.Stage = Configuration.Stage.Get().ToString();
            e.Tenant = ServiceAccessor.Instance?.GetService<ITenantAccessor>()?.Tenant;
            e.Timestamp = logEvent.Timestamp.DateTime;
            e.Level = logEvent.Level.ToString().ToUpper();
            e.Message = logEvent.RenderMessage();
            e.Exception = logEvent.Exception?.ToString();

            _client.Publish(e);
        }
    }
}
