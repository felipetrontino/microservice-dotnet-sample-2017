using Framework.Core.Entities;
using System.Collections.Generic;

namespace Book.Entities
{
    public class BookCategory : EFEntity
    {
        public string Name { get; set; }

        public List<BookCategoryBook> Books { get; set; } = new List<BookCategoryBook>();
    }
}
