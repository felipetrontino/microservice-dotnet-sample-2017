using System;
using System.Collections.Generic;

namespace Framework.Test.Common
{
    public static class FakeHelper
    {
        public static string StringNull => null;

        public static List<T> ListNull<T>() => null;

        public static string StringEmpty => string.Empty;

        public static List<T> ListEmpty<T>() => new List<T>();

        public static string Key { get { return Guid.NewGuid().ToString(); } }

        public static Guid GetId(string key)
        {
            return Guid.Parse(key);
        }

        public static string GetUserName(string key = null)
        {
            return $"{key}_USER";
        }

        public static string GetTenant(string key = null)
        {
            return $"{key}_TENANT";
        }

        public static string GetRequestId(string key = null)
        {
            return key;
        }

        public static string GetLanguage(string key = null)
        {
            return key ?? "EN";
        }
    }
}
