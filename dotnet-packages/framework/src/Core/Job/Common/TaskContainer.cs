using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Job.Common
{
    public class TaskContainer : ITaskContainer
    {
        private readonly List<(string Name, ITask Task)> _tasks = new List<(string Name, ITask Task)>();

        public ITaskContainer Add(string name, ITask taskRunner)
        {
            _tasks.Add((name, taskRunner));

            return this;
        }

        public ITask Get(string name)
        {
            return _tasks.FirstOrDefault(x => x.Name == name).Task;
        }

        public IEnumerable<(int Id, string Name, ITask Task)> GetAll()
        {
            return _tasks.Select((x, i) => (i + 1, x.Name, x.Task)).ToList();
        }

        public ITask GetById(int id)
        {
            return _tasks[id - 1].Task;
        }

        public ConsoleRunner Build()
        {
            return new ConsoleRunner(this);
        }

        public static ITaskContainer Create()
        {
            return new TaskContainer();
        }
    }
}
