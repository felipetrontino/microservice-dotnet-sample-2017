using Framework.Core.Helpers;
using System;

namespace Framework.Core.Extensions
{
    public static class DateTimeExtensions
    {
        private static TimeSpan GetEpochTimeSpan(this DateTime date) => date.Subtract(DateHelper.GetEpochTime());

        public static long GetTimestamp(this DateTime date, bool seconds = false)
        {
            var timeSpan = date.ToUniversalTime().GetEpochTimeSpan();
            return (long)(seconds ? timeSpan.TotalSeconds : timeSpan.TotalMilliseconds);
        }

        public static long GetTotalMinutes(this DateTime date, DateTime endTime)
        {
            return (long)Math.Round((endTime - date).TotalMinutes, MidpointRounding.AwayFromZero);
        }

        public static DateTime TrimMilliseconds(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);

        public static DateTimeOffset ToOffset(this DateTime value)
        {
            var southAmerica = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            var utcOffset = new DateTimeOffset(value, TimeSpan.Zero);
            return utcOffset.ToOffset(southAmerica.GetUtcOffset(utcOffset));
        }
    }
}
