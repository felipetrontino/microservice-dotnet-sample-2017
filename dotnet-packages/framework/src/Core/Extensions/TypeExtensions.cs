namespace Framework.Core.Extensions
{
    public static class TypeExtensions
    {
        public static T Clone<T>(this T item)
        {
            var inst = item.GetType().GetMethod("MemberwiseClone", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            return (T)inst?.Invoke(item, null);
        }
    }
}
