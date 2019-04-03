namespace Framework.Core.Common
{
    public class TenantAccessor : ITenantAccessor
    {
        public TenantAccessor(string tenant = "")
        {
            Tenant = tenant;
        }

        public string Tenant { get; private set; }
    }
}
