using System;

namespace Framework.Core.Config.Consul
{
    public interface IConsulConfigurationSource
    {
        string Path { get; }
        Uri Url { get; }

        bool Optional { get; set; }

        bool ReloadOnChange { get; set; }
    }
}