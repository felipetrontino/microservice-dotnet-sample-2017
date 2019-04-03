using Framework.Core.Common;
using Framework.Core.Config.Consul;
using Framework.Core.Enums;
using Framework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace Framework.Core.Config
{
    public static class Configuration
    {
        public static readonly ValueGetter<Stage> Stage = new ValueGetter<Stage>(() =>
        { return EnumHelper.ParseTo<Stage>(Environment.GetEnvironmentVariable(EnvironmentVariables.Environment)); },
           x => Environment.SetEnvironmentVariable(EnvironmentVariables.Environment, x.GetName()));

        public static readonly ValueGetter<string> Host = new ValueGetter<string>(() =>
        { return Environment.GetEnvironmentVariable(EnvironmentVariables.Hostname); },
            x => Environment.SetEnvironmentVariable(EnvironmentVariables.Hostname, x));

        public static readonly ValueGetter<string> StageName = new ValueGetter<string>(() =>
        { return Stage.Get().GetName().ToLower(); });

        public static readonly ValueGetter<string> Audience = new ValueGetter<string>(() =>
        { return Environment.GetEnvironmentVariable(EnvironmentVariables.Application); },
            x => Environment.SetEnvironmentVariable(EnvironmentVariables.Application, x));

        public static readonly ValueGetter<string> ProjectName = new ValueGetter<string>(() =>
        { return Assembly.GetEntryAssembly().GetName().Name; });

        public static readonly ValueGetter<string> KVServerUrl = new ValueGetter<string>(() =>
        { return Environment.GetEnvironmentVariable(EnvironmentVariables.Consul); },
           x => Environment.SetEnvironmentVariable(EnvironmentVariables.Consul, x));

        public static readonly ValueGetter<bool> Debugging = new ValueGetter<bool>(() =>
        { return Environment.GetEnvironmentVariable(EnvironmentVariables.Debug).ToBoolean(); },
            x => Environment.SetEnvironmentVariable(EnvironmentVariables.Debug, x.ToString()));

        public static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder().PrepareBuilder().Build();
        }

        public static IConfigurationBuilder PrepareBuilder(this IConfigurationBuilder configurationBuilder)
        {
            var url = KVServerUrl.Get();
            var audience = Audience.Get();
            var stage = StageName.Get();

            if (url != null)
            {
                var path = audience != null ? $"{audience}/{stage}" : stage;

                configurationBuilder.AddConsul(url, path, true, true);
            }

            return configurationBuilder;
        }

        private static class EnvironmentVariables
        {
            public const string Environment = "ASPNETCORE_ENVIRONMENT";
            public const string Application = "ASPNETCORE_APPLICATION";
            public const string Consul = "APP_CONSUL";
            public const string Debug = "APP_DEBUGGING";
            public const string Hostname = "APP_HOST";
        }
    }
}
