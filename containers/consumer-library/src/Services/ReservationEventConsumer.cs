﻿using Framework.Core.Bus.Consumer;
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
    public class ReservationEventConsumer : IConsumer<ReservationEventMessage>
    {
        public void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbLibrary>();
            services.AddScoped<IPublishEventService, PublishEventService>();
        }

        public Task ProcessAsync(IServiceProvider provider, ReservationEventMessage message)
        {
            var service = provider.GetService<IPublishEventService>();
            return service.PublishReservationEventAsync(message);
        }
    }
}