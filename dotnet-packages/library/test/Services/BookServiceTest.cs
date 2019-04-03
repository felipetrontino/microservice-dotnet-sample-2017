using FluentAssertions;
using Framework.Test.Common;
using Framework.Test.Data;
using Library.Data;
using Library.Models.Message;
using Library.Services;
using Library.Tests.Mocks.Entities;
using Library.Tests.Mocks.Message;
using Library.Tests.Utils;
using System.Linq;
using Xunit;

namespace Library.Tests.Services
{
    public class BookServiceTest : BaseTest
    {
        protected DbLibrary Db { get; }
        protected IMockRepository MockRepository { get; }

        public BookServiceTest()
        {
            Db = MockHelper.GetDbContext<DbLibrary>();
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

        #endregion

        #region Utils

        private void UpdateAsync(BookMessage message)
        {
            var service = new BookService(Db);
            service.UpdateAsync(message).Wait();
        }

        #endregion 
    }
}
