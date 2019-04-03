using Framework.Test.Common;
using Library.Entities;
using Library.Tests.Utils;

namespace Library.Tests.Mocks.Entities
{
    public class CopyMock : MockBuilder<CopyMock, Copy>
    {
        public static Copy Get(string key)
        {
            return Create(key).Default().Build();
        }

        public CopyMock Default()
        {
            Value.Number = Fake.GetCopyNumber(Key);
            Value.Book = BookMock.Get(Key);

            return this;
        }
    }
}
