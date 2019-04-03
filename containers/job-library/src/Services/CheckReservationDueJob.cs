using Framework.Core.Job.Quartz;
using Library.Core.Interfaces;
using Library.Data;
using Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Library.Job
{
    public class CheckReservationDueJob : BaseJob
    {
        protected override void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbLibrary>();
            services.AddScoped<IReservationService, ReservationService>();
        }

        protected override Task InternalRunAsync(IServiceProvider provider)
        {
            var service = provider.GetService<IReservationService>();
            return service.CheckDueAsync();
        }
    }
}
