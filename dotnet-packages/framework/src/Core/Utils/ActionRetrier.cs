using System;
using System.Threading.Tasks;

namespace Framework.Core.Utils
{
    public class ActionRetrier<TException>
        where TException : Exception
    {
        public int Attempts { get; set; }
        public int Delay { get; set; }
        public Action Task { get; set; }
        public Func<Task> TaskAsync { get; set; }
        public Action<TException> OnAttemptError { get; set; }

        public void Run()
        {
            if (Task == null)
            {
                throw new InvalidOperationException("Invalid task.");
            }

            new FuncRetrier<TException, int>
            {
                Attempts = Attempts,
                Delay = Delay,
                Task = () =>
                {
                    Task();
                    return 1;
                },
                OnAttemptError = OnAttemptError
            }.Run();
        }

        public async Task RunAsync()
        {
            if (TaskAsync == null)
            {
                throw new InvalidOperationException("Invalid async task.");
            }

            await new FuncRetrier<TException, int>
            {
                Attempts = Attempts,
                Delay = Delay,
                TaskAsync = async () =>
                {
                    await TaskAsync();
                    return 1;
                },
                OnAttemptError = OnAttemptError
            }.RunAsync();
        }
    }
}
