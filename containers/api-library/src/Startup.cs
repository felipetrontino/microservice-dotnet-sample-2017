using Framework.Web.Common;
using Library.Core.Interfaces;
using Library.Data;
using Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override void RegisterService(IServiceCollection services)
        {
            base.RegisterService(services);

            services.AddScoped<DbLibrary>();
            services.AddScoped<IReservationService, ReservationService>();
        }
    }
}
