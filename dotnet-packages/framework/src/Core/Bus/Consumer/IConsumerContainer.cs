using Microsoft.Extensions.Configuration;

namespace Framework.Core.Bus.Consumer
{
    public interface IConsumerContainer
    {
        string QueueName { get; }

        string ExchangeName { get; }

        IConsumerContainer Add<TConsumer, TMessage>(string contentName = null)
            where TConsumer : IConsumer<TMessage>
            where TMessage : IBusMessage;

        ConsumerResolver Get(string key);

        ConsumerHandler Build(IConfiguration config);
    }
}
