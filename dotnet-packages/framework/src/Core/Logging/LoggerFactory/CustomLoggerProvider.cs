using Microsoft.Extensions.Logging;
using System;

namespace Framework.Core.Logging.LoggerFactory
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        ~CustomLoggerProvider()
        {
            Dispose(false);
        }

        #endregion IDisposable
    }
}
