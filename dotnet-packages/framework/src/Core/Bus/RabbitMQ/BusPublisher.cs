using Framework.Core.Common;
using Framework.Core.Exceptions;
using Framework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace Framework.Core.Bus.RabbitMQ
{
    public class BusPublisher : BaseBus, IBusPublisher
    {
        public BusPublisher(IConfiguration configuration, ITenantAccessor tenantAccessor, IUserAccessor userAccessor, ILanguageAccessor languageAccessor)
           : base(configuration, tenantAccessor, userAccessor, languageAccessor)
        {
        }

        public async Task PublishAsync<TMessage>(string contextName, TMessage message, Action<IBusOptions> config = null)
            where TMessage : IBusMessage
        {
            var key = Connect(config);

            if (await IsConectedAsync(key))
            {
                using (var channel = Connection.CreateModel())
                {
                    var type = message.GetType();
                    message.UserName = UserAccessor.UserName;
                    message.Tenant = TenantAccessor.Tenant;
                    message.Language = LanguageAccessor.Language;

                    bool isSubscriber = typeof(IBusPublishMessage).IsAssignableFrom(type);

                    if (isSubscriber)
                        channel.ExchangeDeclare(contextName, ExchangeType.Fanout, true, false, null);
                    else
                        channel.QueueDeclare(contextName, true, false, false, null);

                    var props = channel.CreateBasicProperties();
                    props.MessageId = message.MessageId;
                    props.ContentType = message.ContentName;
                    props.DeliveryMode = 2;

                    if (message.RequestId != null)
                        props.CorrelationId = message.RequestId;

                    var json = JsonConvert.SerializeObject(message, new JsonSerializerSettings()
                    {
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                        TypeNameHandling = TypeNameHandling.None,
                    });

                    var messageBytes = json.ToBytes();

                    if (isSubscriber)
                        channel.BasicPublish(contextName, string.Empty, props, messageBytes);
                    else
                    {
                        channel.BasicPublish(string.Empty, contextName, props, messageBytes);
                    }
                }
            }
            else
                throw new HBBusException("BusPublisher could not open connection.");
        }
    }
}