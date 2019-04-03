using Book.Core.Enums;
using Framework.Core.Entities;
using System.Collections.Generic;

namespace Book.Entities
{
    public class Book : EFEntity
    {
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public Language Language { get; set; }

        public BookAuthor Author { get; set; }

        public List<BookCategoryBook> Categories { get; set; } = new List<BookCategoryBook>();
    }
}
