using System;
using System.Globalization;

namespace Framework.Core.Extensions
{
    public static class ParseExtensions
    {
        public static bool ToBoolean(this string input)
        {
            if (bool.TryParse(input, out bool result))
            {
                return result;
            }

            return false;
        }

        public static decimal ToDecimal(this string input)
        {
            return ToDecimal(input, CultureInfo.CurrentCulture);
        }

        public static DateTime FromOaDate(this string value)
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double d);

            if (d != 0)
                return DateTime.FromOADate(d);

            return DateTime.MinValue;
        }

        public static DateTime ToDateTime(this string value)
        {
            if (DateTime.TryParse(value, out DateTime date)) return date;

            return DateTime.MinValue;
        }

        public static decimal ToDecimal(this string input, IFormatProvider provider)
        {
            if (decimal.TryParse(input, NumberStyles.Number, provider, out decimal result))
            {
                return result;
            }

            return 0;
        }

        public static double ToDouble(this string input)
        {
            return ToDouble(input, CultureInfo.CurrentCulture);
        }

        public static double ToDouble(this string input, IFormatProvider provider)
        {
            if (double.TryParse(input, NumberStyles.Float | NumberStyles.AllowThousands, provider, out double result))
            {
                return result;
            }

            return 0;
        }

        public static double? ToNouble(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            return ToDouble(input);
        }

        public static double? ToNDouble(this string input, IFormatProvider provider)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            return ToDouble(input, provider);
        }

        public static short ToShort(this string input)
        {
            return ToShort(input, CultureInfo.CurrentCulture);
        }

        public static short ToShort(this string input, IFormatProvider provider)
        {
            if (short.TryParse(input, NumberStyles.Integer, provider, out short result))
            {
                return result;
            }

            return 0;
        }

        public static int ToInt(this string input)
        {
            return ToInt(input, CultureInfo.CurrentCulture);
        }

        public static int ToInt(this string input, IFormatProvider provider)
        {
            if (int.TryParse(input, NumberStyles.Integer, provider, out int result))
            {
                return result;
            }

            return 0;
        }

        public static int? ToNInt(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            return ToInt(input);
        }

        public static int? ToNInt(this string input, IFormatProvider provider)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            return ToInt(input, provider);
        }
    }
}
