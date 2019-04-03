using Bookstore.Core.Interfaces;
using Bookstore.Data;
using Bookstore.Services;
using Framework.Core.Bus;
using Framework.Core.Bus.RabbitMQ;
using Framework.Web.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Api
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

            services.AddScoped<DbBookstore>();          
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
        }
    }
}
