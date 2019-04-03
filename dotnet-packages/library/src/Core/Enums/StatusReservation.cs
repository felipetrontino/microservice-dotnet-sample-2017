using Framework.Core.Enums;

namespace Library.Core.Enums
{
    public enum StatusReservation
    {
        [EnumInfo("", "Unknown")]        
        Unknown = 0,

        [EnumInfo("O", "Opened")]
        Opened = 1,

        [EnumInfo("D", "Deliveried")]
        Deliveried = 2,

        [EnumInfo("C", "Cancelled")]
        Cancelled = 3,

        [EnumInfo("E", "Expired")]
        Expired = 4
    }
}
