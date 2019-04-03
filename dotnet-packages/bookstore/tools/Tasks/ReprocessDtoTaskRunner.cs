using Bookstore.Core.Common;
using Bookstore.Data;
using Bookstore.Entities;
using Bookstore.Models.Message;
using Framework.Core.Bus;
using Framework.Core.Job.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Tools.Tasks
{
    public class ReprocessDtoTaskRunner : BaseTask
    {
        protected override void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbBookstore>();
        }

        protected override async Task InternalRunAsync(IServiceProvider provider)
        {
            List<Order> orders = null;

            using (var context = provider.GetService<DbBookstore>())
            {
                orders = await context.Orders.ToListAsync();
            }

            foreach (var order in orders)
            {
                var message = new ShippingDtoMessage()
                {
                    OrderId = order.Id
                };

                var bus = provider.GetService<IBusPublisher>();
                await bus.PublishAsync(QueueNames.Bookstore, message);
            }
        }
    }
}
