using Framework.Core.Job.Quartz;
using Quartz;
using System.Threading.Tasks;

namespace Library.Job
{
    public class Program
    {
        protected Program()
        {
        }

        public static async Task<int> Main(string[] args)
        {
            return await QuartzJobBootstrap.RunAsync(args, RunAsync);
        }

        private static async Task RunAsync(IScheduler scheduler)
        {
            var job = JobBuilder.Create<CheckReservationDueJob>().Build();

            var trigger = TriggerBuilder.Create()
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(15)
                  .RepeatForever())
              .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
