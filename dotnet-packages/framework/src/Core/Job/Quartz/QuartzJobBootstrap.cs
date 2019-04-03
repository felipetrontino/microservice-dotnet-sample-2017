using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Core.Logging;
using Framework.Core.Logging.Serilog;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace Framework.Core.Job.Quartz
{
    public static class QuartzJobBootstrap
    {
        public static async Task<int> RunAsync(string[] args, Func<IScheduler, Task> runAsync)
        {
            var config = Configuration.GetConfiguration();
            LogHelper.Logger = new SerilogLogger(config);

            try
            {
                LogHelper.Debug("Starting QuartzJob...");                

                DependencyInjector.RegisterServices(config);                

                var factory = new StdSchedulerFactory();                

                var scheduler = await factory.GetScheduler();

                await scheduler.Start();                

                scheduler.Context.Put(QuartzConfig.Configuration, config);                

                await runAsync(scheduler);

                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("QuartzJob terminated unexpectedly.", ex);
                return 1;
            }
            finally
            {
#if DEBUG
                Console.ReadKey();
#endif
            }
        }
    }
}
