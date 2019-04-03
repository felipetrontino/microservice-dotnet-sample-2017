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
    public class PurchaseConsumer : IConsumer<PurchaseMessage>
    {
        public void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbBookstore>();
            services.AddScoped<IPurchaseService, PurchaseService>();
        }

        public Task ProcessAsync(IServiceProvider provider, PurchaseMessage message)
        {
            var service = provider.GetService<IPurchaseService>();
            return service.CreateAsync(message);
        }
    }
}
