using Bookstore.Core.Interfaces;
using Bookstore.Data;
using Bookstore.Models.Message;
using Bookstore.Services;
using Framework.Core.Bus.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Bookstore.Consumer.Book.Services
{
    public class UpdateBookConsumer : IConsumer<BookMessage>
    {
        public void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbBookstore>();
            services.AddScoped<IBookService, BookService>();
        }

        public async Task ProcessAsync(IServiceProvider provider, BookMessage message)
        {
            var service = provider.GetService<IBookService>();
            await service.UpdateAsync(message);
        }
    }
}
