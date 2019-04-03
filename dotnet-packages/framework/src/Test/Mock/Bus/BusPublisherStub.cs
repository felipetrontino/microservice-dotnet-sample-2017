using Framework.Core.Bus;
using Framework.Core.Config;
using Framework.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Test.Mock.Bus
{
    public class BusPublisherStub : IBusPublisher
    {
        protected Dictionary<string, MockContainer> Containers { get; } = new Dictionary<string, MockContainer>();

        public static BusPublisherStub Create()
        {
            return new BusPublisherStub();
        }

        public async Task PublishAsync<TMessage>(string contextName, TMessage message, Action<IBusOptions> config = null)
            where TMessage : IBusMessage
        {
            IBusOptions options = new BusOptions(ConnectionStringNames.Rabbit);
            config?.Invoke(options);

            await Task.Run(() =>
            {
                Get(options.Key).Enqueue(contextName, message);
            });
        }

        public TMessage Dequeue<TMessage>(string queueName, string connectionStringName = null, string contentName = null)
            where TMessage : IBusMessage
        {
            var queue = GetQueue(queueName, connectionStringName);

            if (!IsMessageValid<TMessage>(queue, contentName)) return default;

            return GetMessage<TMessage>(queue);
        }

        public TMessage DequeueExchange<TMessage>(string exchangeName, string connectionStringName = null, string contentName = null)
           where TMessage : IBusMessage
        {
            var queue = GetQueue(exchangeName, connectionStringName);

            if (!IsMessageValid<TMessage>(queue, contentName, true)) return default;

            return GetMessage<TMessage>(queue);
        }

        private TMessage GetMessage<TMessage>(Queue<IBusMessage> queue)
             where TMessage : IBusMessage
        {
            var ret = queue.Dequeue();

            if (ret == null) return default;

            return (TMessage)ret;
        }

        private bool IsMessageValid<TMessage>(Queue<IBusMessage> queue, string contentName, bool exchange = false)
             where TMessage : IBusMessage
        {
            var messageType = typeof(TMessage);

            var publishType = typeof(IBusPublishMessage);

            contentName = contentName ?? messageType.Name;

            if ((exchange && publishType.IsAssignableFrom(messageType))
                || (!exchange && !publishType.IsAssignableFrom(messageType)))
            {
                return queue.OfType<TMessage>().Any(x => x.ContentName == contentName);
            }

            return false;
        }

        private Queue<IBusMessage> GetQueue(string contextName, string connectionStringName)
        {
            var options = new BusOptions(connectionStringName ?? ConnectionStringNames.Rabbit);
            var key = options.Key;

            return Get(key).GetQueue(contextName);
        }

        private MockContainer Get(string key)
        {
            var container = Containers.FirstOrDefault(x => x.Key == key).Value;

            if (container == null)
            {
                container = new MockContainer(key);
                Containers.Add(key, container);
            }

            return container;
        }

        protected class MockContainer
        {
            public MockContainer(string key)
            {
                Key = key;
            }

            public string Key { get; }
            protected Dictionary<string, Queue<IBusMessage>> Queues { get; } = new Dictionary<string, Queue<IBusMessage>>();

            public void Enqueue(string contextName, IBusMessage message)
            {
                message.Tenant = FakeHelper.GetTenant();

                var queue = GetQueue(contextName);
                queue.Enqueue(message);
            }

            public Queue<IBusMessage> GetQueue(string contextName)
            {
                var queue = Queues.FirstOrDefault(x => x.Key == contextName).Value;
                if (queue == null)
                {
                    queue = new Queue<IBusMessage>();
                    Queues.Add(contextName, queue);
                }

                return queue;
            }
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~BusPublisherStub()
        {
            Dispose(false);
        }

        #endregion IDisposable
    }
}
