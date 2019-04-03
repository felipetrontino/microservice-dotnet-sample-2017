using Framework.Core.Logging;
using Framework.Core.Logging.Serilog;
using System;
using System.Threading.Tasks;

namespace Framework.Core.Common
{
    public static class ConsoleBootstrap
    {
        public static async Task<int> RunAsync(Func<Task> runAsync)
        {
            var config = Config.Configuration.GetConfiguration();
            LogHelper.Logger = new SerilogLogger(config);

            try
            {
                LogHelper.Debug($"Starting Console...");

                DependencyInjector.RegisterServices(config);

                await runAsync();

                LogHelper.Debug($"Exiting Console...");

                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"Console terminated unexpectedly.", ex);

#if DEBUG
                Console.ReadLine();
#endif
                return 1;
            }
        }
    }
}
