using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Core.Logging;
using Framework.Core.Logging.Serilog;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Framework.Core.Bus
{
    public static class ConsumerBootstrap
    {
        public static async Task<int> RunAsync(Func<IConfiguration, Task> runAsync)
        {
            var config = Configuration.GetConfiguration();
            LogHelper.Logger = new SerilogLogger(config);

            try
            {
                LogHelper.Debug("Starting Consumer...");                

                DependencyInjector.RegisterServices(config);

                await runAsync(config);

                LogHelper.Debug("Exiting Consumer...");

                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Consumer terminated unexpectedly.", ex);

#if DEBUG
                Console.ReadLine();
#endif
                return 1;
            }
        }
    }
}
