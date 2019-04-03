using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Framework.Core.Bus.Consumer
{
    public interface IConsumer<in TMessage>
         where TMessage : IBusMessage
    {
        void Configure(IConfiguration config, IServiceCollection services);

        Task ProcessAsync(IServiceProvider provider, TMessage message);
    }
}
