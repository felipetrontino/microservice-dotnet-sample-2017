using System;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Core.Utils
{
    public class FuncRetrier<TException, TResult>
        where TException : Exception
    {
        public int Attempts { get; set; }
        public int Delay { get; set; }
        public Func<TResult> Task { get; set; }
        public Func<Task<TResult>> TaskAsync { get; set; }
        public Action<TException> OnAttemptError { get; set; }

        public TResult Run()
        {
            if (Task == null)
            {
                throw new InvalidOperationException("Invalid task.");
            }

            return RunAsync(() => System.Threading.Tasks.Task.FromResult(Task())).GetAwaiter().GetResult();
        }

        public Task<TResult> RunAsync()
        {
            if (TaskAsync == null)
            {
                throw new InvalidOperationException("Invalid async task.");
            }

            return RunAsync(TaskAsync);
        }

        private async Task<TResult> RunAsync(Func<Task<TResult>> task)
        {
            if (Attempts < 1)
            {
                throw new InvalidOperationException("Invalid attempt number.");
            }

            var currentAttempt = 1;
            TException lastException = null;
            var result = default(TResult);
            var ok = false;

            while (true)
            {
                try
                {
                    result = await task();
                    ok = true;
                    break;
                }
                catch (TException e)
                {
                    lastException = e;
                    currentAttempt++;
                    if (currentAttempt > Attempts)
                    {
                        break;
                    }

                    OnAttemptError?.Invoke(e);
                    Thread.Sleep(Delay);
                }
            }

            if (!ok)
            {
                throw new InvalidOperationException($"Error retrying action {Attempts} times: {lastException.Message}.",
                    lastException);
            }

            return result;
        }
    }
}
