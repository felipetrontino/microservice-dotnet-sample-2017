using Framework.Core.Logging;
using Framework.Core.Job.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Framework.Core.Job.Quartz
{
    [DisallowConcurrentExecution]
    public abstract class BaseJob : BaseTask, IJob

    {
        protected BaseJob()
        {
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var name = context.JobDetail.Key.Name.ToString();

            try
            {
                LogHelper.Debug($"Starting {name}...");

                var watch = Stopwatch.StartNew();

                var config = context.Scheduler.Context[QuartzConfig.Configuration] as IConfiguration;

                await RunAsync(config);

                watch.Stop();

                LogHelper.Debug($"Exiting { name }: { watch.Elapsed }...");
            }
            catch (Exception ex)
            {
                LogHelper.Error($"{name} terminated unexpectedly.", ex);
            }
        }
    }
}
