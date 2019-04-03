using System.Collections.Generic;

namespace Framework.Core.Job.Common
{
    public interface ITaskContainer
    {
        ITaskContainer Add(string name, ITask taskRunner);

        ITask Get(string name);

        ITask GetById(int id);

        IEnumerable<(int Id, string Name, ITask Task)> GetAll();

        ConsoleRunner Build();
    }
}
