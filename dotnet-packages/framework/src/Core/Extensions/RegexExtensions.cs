using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Core.Extensions
{
    public static class RegexExtensions
    {
        public static string RemoveDiacritics(this string value)
        {
            if (value == null) return null;

            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveNumerics(this string value)
        {
            if (value == null) return null;

            return Regex.Replace(value, "\\d", "");
        }

        public static string RemoveAphabetics(this string value)
        {
            if (value == null) return null;

            return Regex.Replace(value, "\\D", "");
        }

        public static string RemoveSpaces(this string value)
        {
            if (value == null) return null;

            return Regex.Replace(value, "\\s+", "");
        }

        public static string RemoveDoubleSpaces(this string value)
        {
            if (value == null) return null;

            return Regex.Replace(value, "\\s+", " ");
        }
    }
}
