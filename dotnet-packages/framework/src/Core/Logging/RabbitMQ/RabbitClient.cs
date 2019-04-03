using Framework.Core.Config;
using Framework.Core.Extensions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Framework.Core.Logging.RabbitMQ
{
    internal class RabbitClient : IDisposable
    {
        private readonly RabbitConfig _config;

        private IConnection _connection;
        private IModel _model;
        private IBasicProperties _properties;

        public RabbitClient(RabbitConfig config)
        {
            _config = config;
            InitializeEndpoint();
        }

        private void InitializeEndpoint()
        {
            var connectionFactory = GetConnectionFactory();
            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();

            _model.QueueDeclare(LogEvent.RouteKey, true, false, false, null);

            _properties = _model.CreateBasicProperties();
            _properties.DeliveryMode = 2;
        }

        private IConnectionFactory GetConnectionFactory()
        {
            var ret = new ConnectionFactory
            {
                HostName = _config.Host,
                Port = _config.Port.ToNInt() ?? AmqpTcpEndpoint.UseDefaultPort,
                UserName = _config.UserName,
                Password = _config.Password,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(2)
            };

            if (_config.VHost != null)
                ret.VirtualHost = _config.VHost;

            return ret;
        }

        public void Publish(LogEvent log)
        {
            var message = JsonConvert.SerializeObject(log, new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                TypeNameHandling = TypeNameHandling.None
            });

            _model.BasicPublish("", LogEvent.RouteKey, _properties, message.ToBytes());
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _model.Dispose();
            _connection.Dispose();
        }

        ~RabbitClient()
        {
            Dispose(false);
        }

        #endregion IDisposable
    }
}
