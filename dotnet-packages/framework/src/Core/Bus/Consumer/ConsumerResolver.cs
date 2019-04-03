using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Framework.Core.Bus.Consumer
{
    public class ConsumerResolver
    {
        public Type HandlerType { get; }
        public Type EventType { get; }

        private ConsumerResolver(Type handlerType, Type eventType = null)
        {
            HandlerType = handlerType;
            EventType = eventType;
        }

        public void Configure(object message, IConfiguration config, IServiceCollection services)
        {
            var consumer = Activator.CreateInstance(HandlerType);
            var concreteType = typeof(IConsumer<>).MakeGenericType(EventType);

            concreteType.GetMethod("Configure").Invoke(consumer, new object[] { config, services });
        }

        public async Task ProcessAsync(object message, IServiceProvider provider)
        {
            var consumer = Activator.CreateInstance(HandlerType);
            var concreteType = typeof(IConsumer<>).MakeGenericType(EventType);

            await (Task)concreteType.GetMethod("ProcessAsync").Invoke(consumer, new object[] { provider, message });
        }

        public static ConsumerResolver New(Type handlerType, Type eventType) => new ConsumerResolver(handlerType, eventType);
    }
}
