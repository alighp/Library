using FluentAssertions;
using Library.Entities;
using Library.Persistence;
using Library.Persistence.Books;
using Library.Persistence.Lendings;
using Library.Persistence.Members;
using Library.Services.Lendings;
using Library.Services.Lendings.Contracts;
using Library.Services.Lendings.Exceptions;
using Library.Services.Tests.Specs.Infrastructure;
using Library.TestTools.Books;
using Library.TestTools.Categories;
using Library.TestTools.Members;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Specs.Lendings.Add
{
    public class FailedWhenMemberAgeOutOfBookAgeRange
    {
        [Feature(title: "", AsA = "من به عنوان کتابدار", InOrderTo = "مدیریت امانت¬دادن کتاب¬ها ", IWantTo = "امانت¬دادن کتاب¬ها را ثبت کنم")]
        [Scenario("ثبت امانت دادن یک کتاب به یکی از اعضای کتابخانه با سن خارج از رده سنی کتاب")]
        public class Successful : EFDataContextDatabaseFixture
        {
            private LendingService sut;
            private EFDataContext context;
            private Book book;
            private Member member;
            private AddLendingDto dto;
            Func<Task> expected;
            public Successful(ConfigurationFixture configuration) : base(configuration)
            {
                context = CreateDataContext();
                var unitOfWork = new EFUnitOfWork(context);
                var repository = new EFLendingRepository(context);
                var memberRepository = new EFMemberRepository(context);
                var bookRepository = new EFBookRepository(context);

                sut = new LendingAppService(repository, unitOfWork, memberRepository, bookRepository);
            }
            [Given("2009/07/02 یک عضو کتابخانه با نام عباس بوعذار و تاریخ تولد" +
                "  و آدرس باسکول نادر وجود دارد")]
            [And("یک کتاب با عنوان شازده کوچولو  و با رده سنی 16 تا 80 سال" +
                " در فهرست کتاب¬ها وجود دارد")]
            private void Given()
            {
                var birthDate = new DateTime(2009, 07, 02);
                member = new MemberBuilder().WithFirstName("عباس")
                                                .WithLastName("بوعذار")
                                                .WithBirthDate(birthDate)
                                                .WithAddress("باسکول نادر")
                                                .Build(context);
                var category = CategoryFactory.GenerateCategory(context, "dummy");
                book = new BookBuilder().WithTitle("شازده کوچولو")
                                            .WithMinAge(16)
                                            .WithMaxAge(80)
                                            .Build(context, category.Id);

            }
            [When("یک کتاب با عنوان شازده کوچولو و با رده سنی 16 تا 80 سال" +
                " به یک عضو کتابخانه با نام عباس بوعذار و تاریخ تولد 2009/07/02 " +
                "و آدرس باسکول نادر با تاریخ برگشت 2021/07/04 امانت داده شود")]
            private void When()
            {
                var returnDate = new DateTime(2021, 07, 04);
                dto = new AddLendingDto
                {
                    BookId = book.Id,
                    MemberId = member.Id,
                    ReturnDate = returnDate
                };
                expected = () => sut.Add(dto);
            }
            [Then("نباید هیچ کتابی به عنوان امانت به فهرست امانت¬ها اضافه گردد")]
            [And("خطای سن اعضا خارج از رده سنی کتاب می¬باشد نمایش داده شود")]
            private void Then()
            {

                expected.Should().ThrowExactly<MemberAgeOutOfBookAgeRangeException>();
            }
            [Fact]
            public void Run()
            {
                Runner.RunScenario(
                    _ => Given(),
                    _ => When(),
                    _ => Then()
                    );
            }
        }
    }
}
