namespace Framework.Core.Enums
{
    public class EnumNamesAttribute : System.Attribute
    {
        public string[] Names { get; private set; }

        public EnumNamesAttribute(params string[] names)
        {
            Names = names;
        }
    }
}