using Bookstore.Models.Message;
using Bookstore.Tests.Utils;
using Framework.Test.Common;

namespace Bookstore.Tests.Mocks.Message
{
    public class DropCopyNumberMessageMock : MockBuilder<DropCopyNumberMessageMock, DropCopyNumberMessage>
    {
        public static DropCopyNumberMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public DropCopyNumberMessageMock Default()
        {
            Value.Number = Fake.GetCopyNumber(Key);

            return this;
        }
    }
}
