using Framework.Core.Config;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;

namespace Framework.Test.Mock.Common
{
    public static class ConfigurationStub
    {
        public static IConfiguration Create(Func<Settings> func)
        {
            var settings = func.Invoke();

            var configurationStub = new Mock<IConfiguration>();
            SetFeatureFlags(configurationStub, Settings.Keys.FeatureFlags, settings.FeatureFlags);
            SetFeatureFlags(configurationStub, Settings.Keys.AppSettings, settings.AppSettings);
            SetFeatureFlags(configurationStub, Settings.Keys.ConnectionStrings, settings.ConnectionStrings);
            SetFeatureFlags(configurationStub, Settings.Keys.Preferences, settings.Preferences);

            return configurationStub.Object;
        }

        private static void SetFeatureFlags<TKey, TValue>(Mock<IConfiguration> mockConfiguration, string sectionName, IDictionary<TKey, TValue> dic)
        {
            var list = new List<IConfigurationSection>();

            foreach (var item in dic)
            {
                var configA = new Mock<IConfigurationSection>();
                configA.Setup(x => x.Key).Returns(item.Key.ToString());
                configA.Setup(x => x.Value).Returns(item.Value.ToString());

                list.Add(configA.Object);
            }

            var configurationSectionStub = new Mock<IConfigurationSection>();
            configurationSectionStub.Setup(x => x.GetChildren()).Returns(list);

            mockConfiguration.Setup(x => x.GetSection(sectionName)).Returns(configurationSectionStub.Object);
        }
    }
}
