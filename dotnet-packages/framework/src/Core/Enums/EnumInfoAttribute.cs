namespace Framework.Core.Enums
{
    public class EnumInfoAttribute : System.Attribute
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public EnumInfoAttribute(string description)
        {
            Description = description;
        }

        public EnumInfoAttribute(string name, string description)
            : this(description)
        {
            Name = name;
        }
    }
}