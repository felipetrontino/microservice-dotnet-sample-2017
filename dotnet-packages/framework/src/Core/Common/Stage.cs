using Framework.Core.Enums;

namespace Framework.Core.Common
{
    public enum Stage
    {
        [EnumInfo("dev", "Development")]
        Development,

        [EnumInfo("prod", "Production")]
        Production,
    }
}
