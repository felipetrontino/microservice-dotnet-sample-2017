using Microsoft.Extensions.Configuration;
using System;

namespace Framework.Core.Config.Consul
{
    public class ConsulConfigurationSource : IConfigurationSource, IConsulConfigurationSource
    {
        public Uri Url { get; }
        public string Path { get; }

        public bool Optional { get; set; } = false;

        public bool ReloadOnChange { get; set; } = false;

        public ConsulConfigurationSource(Uri url, string path)
        {
            Url = url;
            Path = path;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigurationProvider(this);
        }
    }
}
