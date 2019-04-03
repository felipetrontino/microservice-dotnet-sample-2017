using FluentAssertions;
using Bookstore.Data;
using Bookstore.Models.Message;
using Bookstore.Services;
using Bookstore.Tests.Mocks.Entities;
using Bookstore.Tests.Mocks.Message;
using Bookstore.Tests.Utils;
using Framework.Test.Common;
using Framework.Test.Data;
using System.Linq;
using Xunit;

namespace Bookstore.Tests.Services
{
    public class BookServiceTest : BaseTest
    {
        protected DbBookstore Db { get; }
        protected IMockRepository MockRepository { get; }

        public BookServiceTest()
        {
            Db = MockHelper.GetDbContext<DbBookstore>();
            MockRepository = new EFMockRepository(Db);
        }

        #region UpdateAsync

        [Fact]
        public void UpdateAsync_Book_Valid()
        {
            var key = FakeHelper.Key;

            var book = BookMock.Get(key);
            MockRepository.Add(book);

            var message = BookMessageMock.Get(key);
            message.Author = Fake.GetAuthorName(FakeHelper.Key);

            UpdateAsync(message);

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var expected = BookMock.Get(key);
            expected.Author = message.Author;

            entity.Should().BeEquivalentToEntity(expected);
        }

        [Fact]
        public void UpdateAsync_Book_Title_NotExists()
        {
            var key = FakeHelper.Key;

            var book = BookMock.Get(key);
            MockRepository.Add(book);

            var message = BookMessageMock.Get(key);
            message.Title = Fake.GetTitle(FakeHelper.Key);
            message.Author = Fake.GetAuthorName(FakeHelper.Key);

            UpdateAsync(message);

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var expected = BookMock.Get(key);
            entity.Should().BeEquivalentToEntity(expected);
        }

        #endregion UpdateAsync

        #region Utils

        private void UpdateAsync(BookMessage message)
        {
            var service = new BookService(Db);
            service.UpdateAsync(message).Wait();
        }

        #endregion Utils

    }
}
