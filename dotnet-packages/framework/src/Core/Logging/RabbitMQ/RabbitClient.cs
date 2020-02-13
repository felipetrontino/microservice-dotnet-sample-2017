using Framework.Core.Extensions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;

namespace Framework.Core.Logging.RabbitMQ
{
    internal class RabbitClient : IDisposable
    {
        private readonly string _connectionString;
        private IConnection _connection;
        private IModel _model;
        private IBasicProperties _properties;

        public RabbitClient(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            InitializeEndpoint();
        }

        private void InitializeEndpoint()
        {
            var connectionFactory = GetConnectionFactory(_connectionString);
            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();

            _model.QueueDeclare(LogEvent.RouteKey, true, false, false, null);

            _properties = _model.CreateBasicProperties();
            _properties.DeliveryMode = 2;
        }

        private static IConnectionFactory GetConnectionFactory(string connectionString)
        {
            var ret = new ConnectionFactory
            {
                Uri = new Uri(connectionString)
            };

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