using System;

namespace Framework.Core.Logging.RabbitMQ
{
    public class LogEvent
    {
        public const string RouteKey = "LogEvent";
        public string Audience { get; set; }
        public string Logger { get; set; }
        public string Stage { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Tenant { get; set; }
    }
}
