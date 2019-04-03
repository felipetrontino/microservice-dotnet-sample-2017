using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Core.Enums;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core.Job.Common
{
    public class ConsoleRunner
    {
        private readonly ITaskContainer _container;

        public ConsoleRunner(ITaskContainer container)
        {
            _container = container;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"### {Configuration.Audience.Get()} ###");

                var option = Prompt.GetInt(string.Join(Environment.NewLine, _container.GetAll().Select((x, i) => x.Id + ". " + x.Name).Append("0. Exit" + Environment.NewLine)));

                if (option == 0)
                {
                    Console.Clear();
                    return;
                }

                var stage = Prompt.GetString("Stage? [dev, prod]");
                var host = Prompt.GetString("Server? [cnode-dev, cnode-cloud]");
                var tenant = Prompt.GetString("Tenant?");

                Configuration.Stage.Set(EnumHelper.ParseTo<Stage>(stage));
                Configuration.StageName.Set(stage);
                Configuration.Host.Set(host);
                Configuration.KVServerUrl.Set($"http://{host}:8500");

                var config = Configuration.GetConfiguration();                

                var taskRunner = _container.GetById(option);
                taskRunner.Tenant = tenant;

                await taskRunner.RunAsync(config);
                Console.WriteLine("Done. Press a key.");
                Console.ReadKey();
            }
        }
    }
}
