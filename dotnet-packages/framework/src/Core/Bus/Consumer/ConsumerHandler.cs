using Framework.Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Core.Bus.Consumer
{
    public class ConsumerHandler
    {
        private readonly ManualResetEvent _shutdown;
        private readonly IConsumerContainer _container;
        private readonly IConfiguration _config;

        public ConsumerHandler(IConsumerContainer container, IConfiguration config)
        {
            _shutdown = new ManualResetEvent(false);
            _container = container;
            _config = config;
        }

        public async Task RunAsync()
        {
            using (var bus = ServiceAccessor.Instance.GetService<IBusReceiver>())
            {
                await bus.ReceiveAsync(_container.QueueName, _container.ExchangeName, async (message) =>
                {
                    var consumer = _container.Get(message.ContentName);

                    if (consumer == null) return;

                    var messageData = JsonConvert.DeserializeObject(message.Body, consumer.EventType);

                    using (var serviceScope = GetServiceScope(_config, messageData, consumer.Configure))
                    {
                        var provider = serviceScope.ServiceProvider;                        

                        await consumer.ProcessAsync(messageData, provider);
                    }
                });

                _shutdown.WaitOne();
            }
        }

        private IServiceScope GetServiceScope(IConfiguration config, object message, Action<object, IConfiguration, IServiceCollection> configure)
        {
            var services = new ServiceCollection();
            services.AddScoped(x => message as IUserAccessor);
            services.AddScoped(x => message as ITenantAccessor);
            services.AddScoped(x => message as ILanguageAccessor);

            configure?.Invoke(message, config, services);

            DependencyInjector.RegisterServices(services, config);

            var provider = services.BuildServiceProvider();
            return provider.CreateScope();
        }
    }
}
