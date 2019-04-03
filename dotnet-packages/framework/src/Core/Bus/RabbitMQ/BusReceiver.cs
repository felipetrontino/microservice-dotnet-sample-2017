using Framework.Core.Common;
using Framework.Core.Exceptions;
using Framework.Core.Logging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Bus.RabbitMQ
{
    public class BusReceiver : BaseBus, IBusReceiver
    {
        public BusReceiver(IConfiguration configuration, ITenantAccessor tenantAccessor, IUserAccessor userAccessor, ILanguageAccessor languageAccessor)
            : base(configuration, tenantAccessor, userAccessor, languageAccessor)
        {
        }

        public async Task ReceiveAsync(string queueName, string exchangeName, Func<MessageInfo, Task> funcAsync, Action<IBusOptions> config = null)
        {
            var key = Connect(config);

            if (await IsConectedAsync(key))
            {
                var channel = Connection.CreateModel();

                channel.QueueDeclare(queueName, true, false, false, null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                if (exchangeName != null)
                {
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true, false, null);
                    channel.QueueBind(queueName, exchangeName, queueName);
                }

                var consumer = new EventingBasicConsumer(channel);
                consumer.ConsumerCancelled += Consumer_ConsumerCancelled;

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var messageInfo = new MessageInfo();
                    messageInfo.ContentName = ea.BasicProperties.ContentType;
                    messageInfo.MessageId = ea.BasicProperties.MessageId;
                    messageInfo.RequestId = ea.BasicProperties.CorrelationId;
                    messageInfo.Body = Encoding.UTF8.GetString(body);

                    try
                    {
                        await funcAsync(messageInfo);

                        channel.BasicAck(ea.DeliveryTag, false);

                        LogHelper.Debug($"Processed: {queueName}-{messageInfo.MessageId}");
                    }
                    catch (Exception ex)
                    {
                        channel.BasicNack(ea.DeliveryTag, false, true);
                        LogHelper.Debug($"BusConsumer cannot processing message on {queueName}: {ex.Message}.");

                        Task.Delay(500).Wait();
                    }
                };

                channel.BasicConsume(queueName, false, consumer);
            }
            else
                throw new HBBusException("BusPublisher could not open connection.");
        }

        private void Consumer_ConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            LogHelper.Error($"BusConsumer has been cancelled");
            Environment.Exit(1);
        }
    }
}