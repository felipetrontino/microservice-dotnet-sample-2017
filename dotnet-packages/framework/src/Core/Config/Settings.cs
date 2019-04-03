using Framework.Core.Extensions;
using Framework.Core.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Config
{
    public class Settings
    {
        private static readonly object _lock = new object();

        public static Settings Empty => new Settings();

        public static Settings GetInstance(IConfiguration config, string tenant = null)
        {
            lock (_lock)
            {
                var ret = new Settings();

                config = config ?? Configuration.GetConfiguration();
                Bind(config, ret, tenant);

                return ret;
            }
        }

        private static void Bind(IConfiguration config, Settings ret, string tenant = null)
        {
            config = config.TryGetSection(Configuration.Audience.Get()) ?? config;
            config = config.TryGetSection(Configuration.Stage.Get().GetName()) ?? config;

            var generalConfig = config.TryGetSection(Keys.General) ?? config;
            if (generalConfig != null)
            {
                generalConfig.Bind(ret);
            }

            var xTenants = config.TryGetSection(Keys.Tenants)?.Value;
            if (xTenants != null)
                ret.Tenants = JsonHelper.Deserialize<string[]>(xTenants).ToList();

            if (tenant != null)
            {
                config = config.TryGetSection(tenant) ?? config;
                config.TryGetSection(Keys.AppSettings)?.Bind(ret.AppSettings);
                config.TryGetSection(Keys.ConnectionStrings)?.Bind(ret.ConnectionStrings);
                config.TryGetSection(Keys.FeatureFlags)?.Bind(ret.FeatureFlags);
                config.TryGetSection(Keys.Preferences)?.Bind(ret.Preferences);
            }
        }

        public List<string> Tenants { get; set; } = new List<string>();

        public ConcurrentDictionary<string, bool> FeatureFlags { get; set; } = new ConcurrentDictionary<string, bool>();

        public ConcurrentDictionary<string, string> AppSettings { get; set; } = new ConcurrentDictionary<string, string>();

        public ConcurrentDictionary<string, string> ConnectionStrings { get; set; } = new ConcurrentDictionary<string, string>();

        public ConcurrentDictionary<string, string> Preferences { get; set; } = new ConcurrentDictionary<string, string>();

        public static class Keys
        {
            public const string General = "*";
            public const string Preferences = "preferences";
            public const string AppSettings = "appSettings";
            public const string ConnectionStrings = "connectionStrings";
            public const string FeatureFlags = "featureFlags";
            public const string Tenants = "tenants";
        }
    }
}
