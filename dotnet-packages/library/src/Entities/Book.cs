using Framework.Core.Entities;
using Library.Core.Enums;

namespace Library.Entities
{
    public class Book : EFEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public Language Language { get; set; }
    }
}
