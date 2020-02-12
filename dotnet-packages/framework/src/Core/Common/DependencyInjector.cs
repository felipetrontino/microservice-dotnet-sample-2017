using Framework.Core.Auth;
using Framework.Core.Bus;
using Framework.Core.Bus.RabbitMQ;
using Framework.Core.Config;
using Framework.Web.Common;
using Framework.Web.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core.Common
{
    public static class DependencyInjector
    {
        public static void RegisterServices(IConfiguration config)
        {
            var services = new ServiceCollection();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<ITenantAccessor, TenantAccessor>();

            RegisterServices(services, config);
        }

        public static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            Register(services, config);

            services.AddScoped<ICultureAccessor, CultureAccessor>();
            ServiceAccessor.Instance = services.BuildServiceProvider();
        }

        public static void RegisterServicesWeb(IServiceCollection services, IConfiguration config)
        {
            Register(services, config);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserAccessor, HttpContextInfoAccessor>();
            services.AddScoped<ITenantAccessor, HttpContextInfoAccessor>();
            services.AddScoped<IRequestIdAccessor, HttpContextInfoAccessor>();
            services.AddScoped<ICultureAccessor, HttpContextInfoAccessor>();
            services.AddScoped<IJwtAccessor, HttpContextInfoAccessor>();

            ServiceAccessor.Instance = services.BuildServiceProvider();
        }

        private static void Register(IServiceCollection services, IConfiguration config)
        {
            services.Configure<Settings>(config);
            services.AddScoped(x => config);
            services.AddScoped<IBusPublisher, BusPublisher>();
            services.AddScoped<IBusReceiver, BusReceiver>();
            services.AddTransient<HeaderHandler>();
            services.AddHttpClient();
            services.AddHttpClientByServicaName(GenericServices.Callback.Name, GenericServices.Callback.ServiceName);
            services.AddHttpClientByServicaName(GenericServices.Resource.Name, GenericServices.Resource.ServiceName);
        }
    }
}