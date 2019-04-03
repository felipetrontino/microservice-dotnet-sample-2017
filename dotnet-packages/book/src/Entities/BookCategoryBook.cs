using Framework.Core.Entities;

namespace Book.Entities
{
    public class BookCategoryBook : EFEntity
    {
        public Book Book { get; set; }

        public BookCategory Category { get; set; }
    }
}
