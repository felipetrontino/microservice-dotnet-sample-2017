using Framework.Core.Enums;

namespace Bookstore.Core.Enums
{
    public enum StatusOrder
    {
        [EnumInfo("", "Unknown")]        
        Unknown = 0,

        [EnumInfo("O", "Opened")]
        Opened = 1,

        [EnumInfo("S", "Closed")]
        Closed = 2,

        [EnumInfo("C", "Cancelled")]
        Cancelled = 3
    }
}
