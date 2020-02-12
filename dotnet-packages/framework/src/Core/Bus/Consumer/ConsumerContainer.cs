using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Bus.Consumer
{
    public class ConsumerContainer : IConsumerContainer
    {
        private readonly Dictionary<string, ConsumerResolver> _consumers = new Dictionary<string, ConsumerResolver>();

        public ConsumerContainer(string queueName, string exchangeName = null)
        {
            QueueName = queueName;
            ExchangeName = exchangeName;
        }

        public string QueueName { get; }

        public string ExchangeName { get; }

        public IConsumerContainer Add<TConsumer, TMessage>(string contentName = null)
            where TConsumer : IConsumer<TMessage>
            where TMessage : IBusMessage
        {
            AddSubscription(!string.IsNullOrEmpty(contentName) ? contentName : typeof(TMessage).Name, typeof(TConsumer), typeof(TMessage));
            return this;
        }

        public ConsumerResolver Get(string key)
        {
            return _consumers.FirstOrDefault(x => x.Key == key).Value;
        }

        public ConsumerHandler Build(IConfiguration config)
        {
            return new ConsumerHandler(this, config);
        }

        private void AddSubscription(string key, Type handlerType, Type eventType = null)
        {
            _consumers.Add(key, ConsumerResolver.New(handlerType, eventType));
        }

        public static IConsumerContainer Create(string queueName, string exchangeName = null)
        {
            return new ConsumerContainer(queueName, exchangeName);
        }
    }
}