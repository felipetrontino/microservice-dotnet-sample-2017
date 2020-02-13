using Bookstore.Core.Interfaces;
using Bookstore.Data;
using Bookstore.Models.Message;
using Bookstore.Services;
using Framework.Core.Bus.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Bookstore.Consumer.Services
{
    public class ShippingEventConsumer : IConsumer<ShippingEventMessage>
    {
        public void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbBookstore>();
            services.AddScoped<IPublishEventService, PublishEventService>();
        }

        public Task ProcessAsync(IServiceProvider provider, ShippingEventMessage message)
        {
            var service = provider.GetService<IPublishEventService>();
            return service.PublishShippingEventAsync(message);
        }
    }
}