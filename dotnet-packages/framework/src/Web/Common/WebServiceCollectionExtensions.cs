using Framework.Core.Config;
using Framework.Web.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.Web.Common
{
    public static class WebServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddHttpClient(this IServiceCollection services, string name, string url)
        {
            var ret = services.AddHttpClient(name, x => x.BaseAddress = new Uri(url));
            ret.AddHttpMessageHandler<HeaderHandler>();

            return ret;
        }

        public static IHttpClientBuilder AddHttpClientByServicaName(this IServiceCollection services, string name, string serviceName)
        {
            var url = $"http://{Configuration.StageName.Get()}_{serviceName}";
            return AddHttpClient(services, name, url);
        }
    }
}
