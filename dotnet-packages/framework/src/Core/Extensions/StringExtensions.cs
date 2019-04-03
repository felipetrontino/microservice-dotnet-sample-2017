using System;
using System.IO;
using System.Text;

namespace Framework.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string value) => string.IsNullOrEmpty(value);

        public static bool IsEmptyTrim(this string value) => string.IsNullOrWhiteSpace(value);

        public static string DiscardLeadingZeroes(string value)
        {
            if (long.TryParse(value, out long n))
            {
                return n.ToString();
            }

            return value;
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string ToPascalCase(this string value)
        {
            if (value == null) return value;
            if (value.Length < 2) return value.ToUpper();

            var words = value.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            var xBuilder = new StringBuilder();

            foreach (string word in words)
            {
                xBuilder.Append(word.Substring(0, 1).ToUpper() + word.Substring(1));
            }

            return xBuilder.ToString();
        }

        public static string ToCamelCase(this string value)
        {
            if (value == null || value.Length < 2)
                return value;

            var words = value.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            var xBuilder = new StringBuilder();
            xBuilder.Append(words[0].ToLower());

            for (int i = 1; i < words.Length; i++)
            {
                xBuilder.Append(words[i].Substring(0, 1).ToUpper() + words[i].Substring(1));
            }

            return xBuilder.ToString();
        }

        public static byte[] ToBytes(this string value)
        {
            return value == null ? null : Encoding.UTF8.GetBytes(value);
        }

        public static bool ContainsIgnoreCase(this string value, string toCheck, bool ignoreAccent)
        {
            if (value == null || toCheck == null) return false;

            if (ignoreAccent)
                return value.RemoveDiacritics().IndexOf(toCheck.RemoveDiacritics(), StringComparison.InvariantCultureIgnoreCase) >= 0;

            return value.IndexOf(toCheck, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        public static bool ContainsIgnoreCase(this string value, string toCheck)
        {
            return ContainsIgnoreCase(value, toCheck, true);
        }

        public static bool EqualsIgnoreCase(this string value, string toCheck, bool ignoreAccent)
        {
            if (value == null) return false;

            if (ignoreAccent)
                return value.RemoveDiacritics().Equals(toCheck.RemoveDiacritics(), StringComparison.InvariantCultureIgnoreCase);

            return value.Equals(toCheck, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool EqualsIgnoreCase(this string value, string toCheck)
        {
            return EqualsIgnoreCase(value, toCheck, true);
        }

        public static Stream ToStream(this string value)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(value);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
