using Book.Core.Enums;

namespace Book.Tests.Utils
{
    public static class Fake
    {
        public static string GetAuthorName(string key)
        {
            return $"{key}_AUTHOR";
        }

        public static string GetCategoryName(string key)
        {
            return $"{key}_CATEGORY";
        }

        public static string GetCatalogName(string key)
        {
            return $"{key}_CATALOG";
        }

        public static string GetTitle(string key)
        {
            return $"{key}_TITLE";
        }

        public static Language GetLanguage(string key)
        {
            return Language.English;
        }
    }
}
