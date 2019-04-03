using System;

namespace Framework.Test.Common
{
    public abstract class BaseTest : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            MockHelper.DisposeMongoDbRunner();
        }
    }
}
