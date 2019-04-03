using Framework.Core.Bus;
using Framework.Core.Bus.Consumer;
using Library.Consumer.Book.Services;
using Library.Core.Common;
using Library.Models.Message;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Library.Consumer.Book
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
            var handler = ConsumerContainer.Create(QueueNames.Book_Library, ExchangeNames.Book)
                                            .Add<UpdateBookConsumer, BookMessage>("BookUpdateMessage")
                                            .Build(config);

            return handler.RunAsync();
        }
    }
}
