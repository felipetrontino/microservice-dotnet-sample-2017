using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default)
        {
            var isValid = dic.TryGetValue(key, out TValue ret);
            return isValid ? ret : defaultValue;
        }

        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            return Task.WhenAll(Partitioner.Create(source)
                .GetPartitions(dop)
                .Select(partition => Task.Run(async () =>
                {
                    using (partition)
                    {
                        while (partition.MoveNext())
                        {
                            await body(partition.Current);
                        }
                    }
                })));
        }

        public static string ToCsv<T>(this IEnumerable<T> list)
        {
            if (list == null) return string.Empty;

            const string delimiter = ";";

            var sb = new StringBuilder();
            var properties = typeof(T).GetProperties();

            MakeHeader(properties, sb, delimiter);

            foreach (var item in list)
                MakeRows(properties, item, sb, delimiter);

            return sb.ToString();
        }

        private static void MakeRows<T>(IReadOnlyList<PropertyInfo> properties, T item, StringBuilder sb, string delimiter)
        {
            for (var index = 0; index < properties.Count; index++)
            {
                var property = properties[index];
                var propertyValue = item.GetType().GetProperty(property.Name)?.GetValue(item, null);

                if (propertyValue != null)
                {
                    var value = propertyValue.ToString().Trim();

                    if (property.PropertyType == typeof(bool))
                        value = (value == "True" ? "Sim" : "Não");
                    else if (property.PropertyType == typeof(DateTime))
                        value = Convert.ToDateTime(value).ToString("dd/MM/yyyy HH:mm:ss");

                    sb.Append(value);
                }

                if (index < (properties.Count - 1))
                    sb.Append(delimiter);
            }

            sb.AppendLine();
        }

        private static void MakeHeader(IReadOnlyList<PropertyInfo> properties, StringBuilder sb, string delimiter)
        {
            for (var index = 0; index < properties.Count; index++)
            {
                var property = properties[index];
                var columnName = property.Name;

                var displayName = property.GetCustomAttributes<DisplayNameAttribute>();

                if (displayName.Any())
                    columnName = displayName.FirstOrDefault().DisplayName;

                sb.Append(columnName);

                if (index < (properties.Count - 1))
                    sb.Append(delimiter);
            }

            sb.AppendLine();
        }
    }
}
