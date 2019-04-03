using Framework.Core.Bus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Core.Extensions
{
    public static class BusExtensions
    {
        public static async Task PublishAllAsync<TMessage>(this IBusPublisher publisher, string contextName, IEnumerable<TMessage> messages) 
            where TMessage : IBusMessage
        {
            foreach (var item in messages)
            {
                await publisher.PublishAsync(contextName, item);
            }
        }
    }
}
