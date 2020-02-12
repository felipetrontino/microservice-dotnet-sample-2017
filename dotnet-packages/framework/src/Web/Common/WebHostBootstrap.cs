using Framework.Core.Config;
using Framework.Core.Extensions;
using Framework.Core.Logging;
using Framework.Core.Logging.Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Framework.Web.Common
{
    public static class WebHostBootstrap
    {
        public static async Task<int> RunAsync<T>()
            where T : class
        {
            var config = Configuration.GetConfiguration();
            LogHelper.Logger = new SerilogLogger(config);

            try
            {
                LogHelper.Debug("Starting WebHost...");

                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        env.EnvironmentName = Configuration.Stage.Get().GetDescription();

                        configurationBuilder.AddConfiguration(config);
                    })
                    .UseStartup<T>()
                    .Build();

                await host.RunAsync();

                LogHelper.Debug("Exiting WebHost...");

                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("WebHost terminated unexpectedly.", ex);

#if DEBUG
                Console.ReadLine();
#endif
                return 1;
            }
        }
    }
}