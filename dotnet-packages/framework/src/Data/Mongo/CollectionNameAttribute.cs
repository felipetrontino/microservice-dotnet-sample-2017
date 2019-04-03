using System;

namespace Framework.Data.Mongo
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionNameAttribute : System.Attribute
    {
        public string Name { get; private set; }

        public CollectionNameAttribute(string name)
        {
            Name = name;
        }
    }
}
