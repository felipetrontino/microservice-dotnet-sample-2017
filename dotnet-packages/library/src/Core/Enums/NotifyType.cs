using Framework.Core.Enums;

namespace Library.Core.Enums
{
    public enum NotifyType
    {
        [EnumInfo("", "Reservation Confirmed")]
        ReservationConfirmed = 0,

        [EnumInfo("", "Reservation Cancelled")]
        ReservationCancelled = 1,

        [EnumInfo("", "Reservation Expired")]
        ReservationExpired = 2,
    }
}
