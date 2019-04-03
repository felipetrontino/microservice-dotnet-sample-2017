using Book.Core.Interfaces;
using Book.Data;
using Book.Models.Message;
using Book.Services;
using Framework.Core.Bus.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Book.Consumer.Services
{
    public class BookConsumer : IConsumer<BookMessage>
    {
        public void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbBook>();
            services.AddScoped<IBookService, BookService>();
        }

        public Task ProcessAsync(IServiceProvider provider, BookMessage message)
        {
            var service = provider.GetService<IBookService>();
            return service.SaveAsync(message);
        }
    }
}
