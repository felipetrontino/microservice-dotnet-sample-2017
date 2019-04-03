using System;
using System.Threading.Tasks;

namespace Framework.Core.Bus
{
    public interface IBusReceiver : IDisposable
    {
        Task ReceiveAsync(string queueName, string exchangeName, Func<MessageInfo, Task> funcAsync, Action<IBusOptions> config = null);
    }
}
