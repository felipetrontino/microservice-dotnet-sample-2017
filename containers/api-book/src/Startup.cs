using Book.Core.Interfaces;
using Book.Data;
using Book.Services;
using Framework.Web.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Api
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration config)
            : base(config)
        {
        }

        protected override void RegisterService(IServiceCollection services)
        {
            base.RegisterService(services);

            services.AddScoped<DbBook>();          
            services.AddScoped<IBookService, BookService>();
        }
    }
}
