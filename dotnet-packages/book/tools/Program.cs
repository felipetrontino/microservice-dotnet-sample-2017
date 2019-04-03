using Book.Tools.Tasks;
using Framework.Core.Common;
using Framework.Core.Job.Common;
using System.Threading.Tasks;

namespace Book.Tools
{
    public class Program
    {
        protected Program()
        {
        }

        public static async Task<int> Main(string[] args)
        {
            return await ConsoleBootstrap.RunAsync(RunAsync);
        }

        private static async Task RunAsync()
        {
            var runner = TaskContainer.Create()
                                      .Add("Migration", new MigrationTaskRunner())
                                      .Build();

            await runner.RunAsync();
        }
    }
}
