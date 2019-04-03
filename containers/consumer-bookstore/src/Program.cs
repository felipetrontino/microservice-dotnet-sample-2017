using Bookstore.Consumer.Services;
using Bookstore.Core.Common;
using Bookstore.Models.Message;
using Framework.Core.Bus;
using Framework.Core.Bus.Consumer;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Bookstore.Consumer
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
            var handler = ConsumerContainer.Create(QueueNames.Bookstore)
                                            .Add<PurchaseConsumer, PurchaseMessage>()
                                            .Add<ShippingDtoConsumer, ShippingDtoMessage>()
                                            .Build(config);

            return handler.RunAsync();
        }
    }
}
