using Framework.Core.Bus.Consumer;
using Library.Core.Interfaces;
using Library.Data;
using Library.Models.Message;
using Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Library.Consumer.Services
{
    public class ReservationExpiredConsumer : IConsumer<ReservationExpiredMessage>
    {
        public void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbLibrary>();
            services.AddScoped<IReservationService, ReservationService>();
        }

        public Task ProcessAsync(IServiceProvider provider, ReservationExpiredMessage message)
        {
            var service = provider.GetService<IReservationService>();
            return service.ExpireAsync(message);
        }
    }
}