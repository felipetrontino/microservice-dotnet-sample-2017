using Framework.Core.Bus.Consumer;
using Library.Core.Interfaces;
using Library.Data;
using Library.Models.Message;
using Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Library.Consumer.Book.Services
{
    public class UpdateBookConsumer : IConsumer<BookMessage>
    {
        public void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbLibrary>();
            services.AddScoped<IBookService, BookService>();
        }

        public async Task ProcessAsync(IServiceProvider provider, BookMessage message)
        {
            var service = provider.GetService<IBookService>();
            await service.UpdateAsync(message);
        }
    }
}
