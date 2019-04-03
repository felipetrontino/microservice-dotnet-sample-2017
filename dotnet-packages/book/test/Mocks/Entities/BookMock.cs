using Book.Tests.Utils;
using Framework.Test.Common;

namespace Book.Tests.Mocks.Entities
{
    public class BookMock : MockBuilder<BookMock, Book.Entities.Book>
    {
        public static Book.Entities.Book Get(string key)
        {
            return Create(key).Default().Build();
        }

        public BookMock Default()
        {
            Value = MockHelper.CreateModel<Book.Entities.Book>(Key);
            Value.AuthorName = Fake.GetAuthorName(Key);
            Value.Title = Fake.GetTitle(Key);
            Value.Language = Fake.GetLanguage(Key);

            return this;
        }

        public BookMock WithCategory()
        {
            Value.Categories.Add(BookCategoryBookMock.Get(Value, BookCategoryMock.Get(Key)));

            return this;
        }
    }
}
