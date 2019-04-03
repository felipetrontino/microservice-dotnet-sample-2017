using Framework.Test.Common;
using Library.Entities;
using Library.Tests.Utils;

namespace Library.Tests.Mocks.Entities
{
    public class BookMock : MockBuilder<BookMock, Book>
    {
        public static Book Get(string key)
        {
            return Create(key).Default().Build();
        }

        public BookMock Default()
        {
            Value.Author = Fake.GetAuthorName(Key);
            Value.Title = Fake.GetTitle(Key);
            Value.Language = Fake.GetLanguage(Key);

            return this;
        }
    }
}
