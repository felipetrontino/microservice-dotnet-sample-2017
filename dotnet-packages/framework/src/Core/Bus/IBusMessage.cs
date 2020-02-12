using Framework.Core.Common;

namespace Framework.Core.Bus
{
    public interface IBusMessage : IBusInfo, IUserAccessor, ITenantAccessor, ICultureAccessor
    {
        void Setup(string userName = null, string tenant = null, string culture = null);
    }
}