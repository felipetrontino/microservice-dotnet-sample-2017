using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<string> GetNames(this Enum value)
        {
            var enumInfoAtt = value.GetAttributes<EnumInfoAttribute>().FirstOrDefault();

            if (enumInfoAtt != null && !string.IsNullOrWhiteSpace(enumInfoAtt.Name))
            {
                yield return enumInfoAtt.Name;
            }
            else
                yield return value.ToString();

            var enumNamesAtt = value.GetAttributes<EnumNamesAttribute>();

            foreach (var item in enumNamesAtt)
            {
                foreach (var name in item.Names)
                {
                    if (!string.IsNullOrWhiteSpace(name))
                        yield return name;
                }
            }
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Enum value)
            where TAttribute : class
        {
            var atts = value.GetType().GetMember(value.ToString())?.FirstOrDefault().GetCustomAttributes(typeof(TAttribute), false);

            if (atts == null) yield break;

            foreach (var item in atts)
            {
                yield return item as TAttribute;
            }
        }

        public static string GetName(this Enum value)
        {
            return GetNames(value).FirstOrDefault();
        }

        public static string GetDescription(this Enum value)
        {
            var attr = value.GetAttributes<EnumInfoAttribute>().FirstOrDefault();

            return attr != null && !string.IsNullOrWhiteSpace(attr.Description) ? attr.Description : value.ToString();
        }
    }
}