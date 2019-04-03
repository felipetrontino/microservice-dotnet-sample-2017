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
    public class ReservationDtoConsumer : IConsumer<ReservationDtoMessage>
    {
        public void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbLibrary>();
            services.AddScoped<IProcessDtoService, ProcessDtoService>();
        }

        public Task ProcessAsync(IServiceProvider provider, ReservationDtoMessage message)
        {
            var service = provider.GetService<IProcessDtoService>();
            return service.CreateReservationAsync(message);
        }
    }
}
