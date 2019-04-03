using Microsoft.Extensions.Configuration;
using System;

namespace Framework.Core.Config.Consul
{
    public static class ConsulConfigurationExtensions
    {
        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder configurationBuilder, string consulUrl, string consulPath, bool optional, bool reloadOnChange)
        {
            var source = new ConsulConfigurationSource(new Uri(consulUrl), consulPath) { Optional = optional, ReloadOnChange = reloadOnChange };
            return configurationBuilder.Add(source);
        }
    }
}
