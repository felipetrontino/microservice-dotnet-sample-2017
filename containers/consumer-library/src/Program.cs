using Framework.Core.Bus;
using Framework.Core.Bus.Consumer;
using Library.Consumer.Services;
using Library.Core.Common;
using Library.Models.Message;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Library.Consumer
{
    internal class Program
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
            var handler = ConsumerContainer.Create(QueueNames.Library)
                                            .Add<ReservationConsumer, ReservationMessage>()
                                            .Add<ReservationDtoConsumer, ReservationDtoMessage>()
                                            .Add<ReservationExpireConsumer, ReservationExpiredMessage>()                                           
                                            .Build(config);

            return handler.RunAsync();
        }
    }
}
