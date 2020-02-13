using Bookstore.Consumer.Book.Services;
using Bookstore.Core.Common;
using Bookstore.Models.Message;
using Framework.Core.Bus;
using Framework.Core.Bus.Consumer;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Bookstore.Consumer.Book
{
    public class Program
    {
        protected Program()
        {
        }

        public static async Task<int> Main(string[] args)
        {
            return await ConsumerBootstrap.RunAsync(RunAsync);
        }

        private static Task RunAsync(IConfiguration config)
        {
            var handler = ConsumerContainer.Create(QueueNames.Book_Bookstore, ExchangeNames.Book)
                                                .Add<UpdateBookConsumer, BookMessage>("UpdateBookEvent")
                                                .Build(config);

            return handler.RunAsync();
        }
    }
}