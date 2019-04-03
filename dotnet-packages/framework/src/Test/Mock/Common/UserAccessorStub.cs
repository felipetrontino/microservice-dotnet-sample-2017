using Framework.Core.Common;
using Framework.Test.Common;

namespace Framework.Test.Mock.Common
{
    public class UserAccessorStub : IUserAccessor
    {
        public string UserName => FakeHelper.GetUserName();

        public static IUserAccessor Create()
        {
            return new UserAccessorStub();
        }
    }
}
