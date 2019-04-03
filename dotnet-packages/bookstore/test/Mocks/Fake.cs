using Bookstore.Core.Enums;
using System;

namespace Bookstore.Tests.Utils
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

        public static string GetOrderNumber(string key)
        {
            return $"{key}_ORDER";
        }

        public static StatusOrder GetStatusOrder(string key)
        {
            return StatusOrder.Opened;
        }

        public static DateTime GetCreateDate(string key)
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

        public static string GetCustomerName(string key)
        {
            return $"{key}_CUSTOMER";
        }

        public static string GetOrderItemName(string key)
        {
            return $"{key}_ITEM";
        }

        public static string GetCopyNumber(string key)
        {
            return $"{key}_COPY";
        }

        public static DateTime GetOrderDate(string key)
        {
            return DateTime.UtcNow.Date;
        }

        public static double GetPrice(string key)
        {
            return 1;
        }
        public static int GetQuantity(string key)
        {
            return 1;
        }
        public static double GetTotal(string key)
        {
            return 1;
        }
    }
}
