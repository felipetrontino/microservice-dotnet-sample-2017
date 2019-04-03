using Book.Entities;
using Book.Tests.Utils;
using Framework.Test.Common;

namespace Book.Tests.Mocks.Entities
{
    public class BookCategoryMock : MockBuilder<BookCategoryMock, BookCategory>
    {
        public static BookCategory Get(string key)
        {
            return Create(key).Default().Build();
        }

        public BookCategoryMock Default()
        {
            Value.Name = Fake.GetCategoryName(Key);

            return this;
        }
    }
}
