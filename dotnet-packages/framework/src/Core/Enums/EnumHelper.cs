using Framework.Core.Extensions;
using System;
using System.Linq;

namespace Framework.Core.Enums
{
    public static class EnumHelper
    {
        public static TEnum ParseTo<TEnum>(string value)
            where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(value)) return default;

            if (Enum.TryParse(value, true, out TEnum ret))
            {
                return ret;
            }

            var values = Enum.GetValues(typeof(TEnum));

            if (values != null)
            {
                foreach (var item in values)
                {
                    if (item is Enum enumerator)
                    {
                        var isValid = enumerator.GetNames().Any(x => x.Equals(value, StringComparison.InvariantCultureIgnoreCase));

                        if (isValid)
                            return (TEnum)item;
                    }
                }
            }

            return default;
        }
    }
}
