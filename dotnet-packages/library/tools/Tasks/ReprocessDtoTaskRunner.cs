using Framework.Core.Bus;
using Framework.Core.Job.Common;
using Library.Core.Common;
using Library.Data;
using Library.Entities;
using Library.Models.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Tools.Tasks
{
    public class ReprocessDtoTaskRunner : BaseTask
    {
        protected override void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbLibrary>();
        }

        protected override async Task InternalRunAsync(IServiceProvider provider)
        {
            List<Reservation> orders = null;

            using (var context = provider.GetService<DbLibrary>())
            {
                orders = await context.Reservations.ToListAsync();
            }

            foreach (var order in orders)
            {
                var message = new ReservationEventMessage()
                {
                    ReservationId = order.Id
                };

                var bus = provider.GetService<IBusPublisher>();
                await bus.PublishAsync(QueueNames.Library, message);
            }
        }
    }
}