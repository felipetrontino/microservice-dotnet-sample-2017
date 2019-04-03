using Framework.Core.Common;
using Framework.Test.Common;

namespace Framework.Test.Mock.Common
{
    public class TenantAccessorStub : ITenantAccessor
    {
        public string Tenant => FakeHelper.GetTenant();

        public static ITenantAccessor Create()
        {
            return new TenantAccessorStub();
        }
    }
}
