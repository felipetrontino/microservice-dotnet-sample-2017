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
    public class ShippingDtoConsumer : IConsumer<ShippingDtoMessage>
    {
        public void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbBookstore>();
            services.AddScoped<IProcessDtoService, ProcessDtoService>();
        }

        public Task ProcessAsync(IServiceProvider provider, ShippingDtoMessage message)
        {
            var service = provider.GetService<IProcessDtoService>();
            return service.CreateShippingAsync(message);
        }
    }
}
