using System;
using System.Threading.Tasks;

namespace Framework.Core.Bus
{
    public interface IBusPublisher : IDisposable
    {
        Task PublishAsync<TMessage>(string contextName, TMessage message, Action<IBusOptions> config = null) where TMessage : IBusMessage;
    }
}
