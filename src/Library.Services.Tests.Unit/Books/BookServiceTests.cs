using FluentAssertions;
using Library.Infrastructure.Test;
using Library.Persistence;
using Library.Services.Books.Contracts;
using Library.Services.Categories.Exceptions;
using Library.TestTools.Books;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.Books
{
    public class BookServiceTests
    {
        private readonly EFDataContext context;
        private readonly BookService sut;
        public BookServiceTests()
        {
            var db = new EFInMemoryDatabase();
            context = db.CreateDataContext<EFDataContext>();
            sut = BookFactory.CreateService(context);
        }
        [Theory]
        [InlineData(-1)]
        public void Add_throw_exception_when_categoryId_inValid(int inValidCategoryId)
        {
            var lending = BookFactory.GenerateAddBookDto(inValidCategoryId);

            Func<Task> expected = () => sut.Add(lending);

            expected.Should().ThrowExactly<BookCategoryNotFoundException>();
        }
    }
}
