using Library.Core.Enums;
using System;

namespace Library.Tests.Utils
{
    public static class Fake
    {
        public static string GetAuthorName(string key)
        {
            return $"{key}_AUTHOR";
        }

        public static string GetTitle(string key)
        {
            return $"{key}_TITLE";
        }

        public static Language GetLanguage(string key)
        {
            return Language.English;
        }

        public static string GetReservationNumber(string key)
        {
            return $"{key}_ORDER";
        }

        public static StatusReservation GetStatusReservation(string key)
        {
            return StatusReservation.Opened;
        }

        public static DateTime GetRequestDate(string key)
        {
            return DateTime.UtcNow.Date;
        }

        public static DateTime GetDueDate(string key)
        {
            return DateTime.UtcNow.AddDays(7);
        }

        public static DateTime GetReturnDate(string key)
        {
            return DateTime.UtcNow.Date;
        }

        public static string GetAddress(string key)
        {
            return $"{key}_ADDRESS";
        }

        public static string GetCity(string key)
        {
            return $"{key}_CITY";
        }

        public static string GetState(string key)
        {
            return $"{key}_STATE";
        }

        public static string GetEmail(string key)
        {
            return $"{key}_EMAIL";
        }

        public static string GetPhone(string key)
        {
            return $"{key}_PHONE";
        }

        public static string GetMemberName(string key)
        {
            return $"{key}_MEMBER";
        }

        public static string GetCopyNumber(string key)
        {
            return $"{key}_COPY";
        }

        public static NotifyType GetNotifyType(string key)
        {
            return NotifyType.ReservationExpired;
        }

        public static string GetTo(string key)
        {
            return $"{key}_TO";
        }

        public static string GetType(string key)
        {
            return $"{key}_TYPE";
        }

        public static string GetMessage(string key)
        {
            return $"{key}_MESSAGE";
        }

        public static string GetUserName(string key)
        {
            return $"{key}_USERNAME";
        }

        public static string GetItemName(string key)
        {
            return $"{key}_ITEMNAME";
        }

        public static string GetItemNumber(string key)
        {
            return $"{key}_ITEMNUMBER";
        }
    }
}
