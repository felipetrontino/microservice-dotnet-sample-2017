using Framework.Core.Common;
using Framework.Test.Common;

namespace Framework.Test.Mock.Common
{
    public class RequestIdAccessorStub : IRequestIdAccessor
    {
        public string RequestId  => FakeHelper.GetRequestId();

        public static IRequestIdAccessor Create()
        {
            return new RequestIdAccessorStub();
        }
    }
}
