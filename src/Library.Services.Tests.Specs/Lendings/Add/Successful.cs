using FluentAssertions;
using Library.Entities;
using Library.Persistence;
using Library.Persistence.Books;
using Library.Persistence.Lendings;
using Library.Persistence.Members;
using Library.Services.Lendings;
using Library.Services.Lendings.Contracts;
using Library.Services.Tests.Specs.Infrastructure;
using Library.TestTools.Books;
using Library.TestTools.Categories;
using Library.TestTools.Members;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Specs.Lendings.Add
{
    [Feature(title: "", AsA = "من به عنوان کتابدار", InOrderTo = "مدیریت امانت¬دادن کتاب¬ها ", IWantTo = "امانت¬دادن کتاب¬ها را ثبت کنم")]
    [Scenario("ثبت امانت دادن یک کتاب به یکی از اعضای کتابخانه")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private LendingService sut;
        private EFDataContext context;
        private Book book;
        private Member member;
        private AddLendingDto dto;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFLendingRepository(context);
            var memberRepository = new EFMemberRepository(context);
            var bookRepository = new EFBookRepository(context);

            sut = new LendingAppService(repository, unitOfWork, memberRepository, bookRepository);
        }
        [Given("یک عضو کتابخانه با نام علی قناعت پیشه و تاریخ تولد 2000/07/02")]
        [And("یک کتاب با عنوان شازده کوچولو و با رده سنی 16 تا 80 سال" +
            " در فهرست کتابها وجود دارد")]
        private void Given()
        {
            var birthDate = new DateTime(1993, 07, 02);
            member = new MemberBuilder().WithFirstName("علی")
                                            .WithLastName("قناعت پیشه")
                                            .WithBirthDate(birthDate)
                                            .Build(context);
            book = new BookBuilder().WithTitle("شازده کوچولو")
                                        .WithMinAge(16)
                                        .WithMaxAge(80)
                                        .Build(context);

        }
        [When("یک کتاب با عنوان شازده کوچولو و با رده سنی 16 تا 80 سال" +
            " به یک عضو کتابخانه با نام علی قناعت پیشه و تاریخ تولد 1993/07/02" +
            "  با تاریخ برگشت 2021/07/04 امانت داده شود")]
        private async Task When()
        {
            var returnDate = new DateTime(2021, 07, 04);
            dto = new AddLendingDto
            {
                BookId = book.Id,
                MemberId = member.Id,
                ReturnDate = returnDate
            };
            await sut.Add(dto);
        }
        [Then(": باید در فهرست امانت¬ها تنها یک کتاب با عنوان شازده کوچولو" +
            " و با رده سنی 16تا80 سال به یک عضو کتابخانه با نام علی قناعت پیشه" +
            " و تاریخ تولد 1993/07/02 با" +
            " تاریخ برگشت 04/07/2021 وجود داشته باشد")]
        private void Then()
        {
            var expected = context.Lendings.First();
            expected.ReturnDate.Should().Be(dto.ReturnDate);
            expected.Book.Title.Should().Be("شازده کوچولو");
            expected.Book.MinAge.Should().Be(16);
            expected.Book.MaxAge.Should().Be(80);
            expected.member.FirstName.Should().Be("علی");
            expected.member.LastName.Should().Be("قناعت پیشه");
            expected.member.BirthDate.Should().Be(new DateTime(1993,07,02));

            expected.ReturnDate.Should().Be(dto.ReturnDate);
        }
        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then()
                );
        }
    }
}
