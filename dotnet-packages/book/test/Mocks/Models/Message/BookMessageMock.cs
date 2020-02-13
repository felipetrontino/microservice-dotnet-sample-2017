using Book.Models.Message;
using Book.Tests.Utils;
using Framework.Test.Common;

namespace Book.Tests.Mocks.Models.Message
{
    public class BookMessageMock : MockBuilder<BookMessageMock, BookMessage>
    {
        public static BookMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public BookMessageMock Default()
        {
            Value.Author = Fake.GetAuthorName(Key);
            Value.Title = Fake.GetTitle(Key);
            Value.Language = Fake.GetLanguage(Key);
            Value.Catalog = Fake.GetCatalogName(Key);

            return this;
        }

        public BookMessageMock WithCategory()
        {
            Value.Category = Fake.GetCategoryName(Key);

            return this;
        }
    }
}