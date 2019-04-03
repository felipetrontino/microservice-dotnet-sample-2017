using Book.Consumer.Services;
using Book.Core.Common;
using Book.Models.Message;
using Framework.Core.Bus;
using Framework.Core.Bus.Consumer;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Book.Consumer
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

        private static Task RunAsync(IConfiguration configuration)
        {
            var handler = ConsumerContainer.Create(QueueNames.Book)
                                            .Add<BookConsumer, BookMessage>()
                                            .Build(configuration);

            return handler.RunAsync();
        }
    }
}
