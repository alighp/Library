using FluentAssertions;
using Library.Infrastructure.Test;
using Library.Persistence;
using Library.Services.Books.Contracts;
using Library.Services.Books.Exceptions;
using Library.Services.Lendings.Contracts;
using Library.Services.Lendings.Exceptions;
using Library.Services.Members.Contracts;
using Library.TestTools.Books;
using Library.TestTools.Lendings;
using Library.TestTools.Members;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.Lendings
{
    public class LendingServiceTests
    {
        private readonly EFDataContext context;
        private readonly LendingService sut;
        //private readonly Mock<IDateTimeService> dateTimeServiceMock;
        public LendingServiceTests()
        {
            var db = new EFInMemoryDatabase();
            context = db.CreateDataContext<EFDataContext>();
            sut = LendingFactory.CreateService(context);
            //dateTimeServiceMock = new Mock<IDateTimeService>();
        }
        [Theory]
        [InlineData(-1)]
        public void Add_throw_exception_when_bookId_inValid(int invalidBookId) {
            var member = MemberFactory.GenerateMember(context);
            var returnDate = new DateTime(2021,07,10);
            var lending = LendingFactory.GenerateAddLendingDto(returnDate,member.Id,invalidBookId);

            Func<Task> expected = () => sut.Add(lending);

            expected.Should().ThrowExactly<BookNotFoundException>();
        }
        [Theory]
        [InlineData(-1)]
        public void Add_throw_exception_when_memberId_inValid(int inValidMemberId)
        {
            var book = BookFactory.GenerateBook(context);
            var returnDate = new DateTime(2021, 07, 10);
            var lending = LendingFactory.GenerateAddLendingDto(returnDate, inValidMemberId, book.Id);

            Func<Task> expected = () => sut.Add(lending);

            expected.Should().ThrowExactly<MemberNotFoundException>();
        }
        [Theory]
        [InlineData(-1)]
        public void Update_throw_exception_when_LendingId_inValid(int inValidMemberId)
        {

            Func<Task> expected = () => sut.UpdateDeliveryDate(inValidMemberId);

            expected.Should().ThrowExactly<LendingNotFoundException>();
        }
    }
}
