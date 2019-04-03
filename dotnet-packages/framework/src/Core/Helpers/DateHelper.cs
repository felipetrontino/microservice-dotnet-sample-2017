using Framework.Core.Extensions;
using System;

namespace Framework.Core.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetEpochTime() => new DateTime(1970, 1, 1);

        public static long GetTimestamp(bool seconds = false) => DateTime.UtcNow.GetTimestamp(seconds);
    }
}
