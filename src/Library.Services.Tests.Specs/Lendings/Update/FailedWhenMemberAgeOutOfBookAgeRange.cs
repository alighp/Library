using FluentAssertions;
using Library.Entities;
using Library.Persistence;
using Library.Persistence.Books;
using Library.Persistence.Lendings;
using Library.Persistence.Members;
using Library.Services.Lendings;
using Library.Services.Lendings.Contracts;
using Library.Services.Lendings.Exceptions;
using Library.Services.Members.Contracts;
using Library.Services.Tests.Specs.Infrastructure;
using Library.TestTools.Books;
using Library.TestTools.Categories;
using Library.TestTools.Lendings;
using Library.TestTools.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Specs.Lendings.Update
{
    public class FailedWhenMemberAgeOutOfBookAgeRange
    {
        [Feature(title: "", AsA = "من به عنوان کتابدار", InOrderTo = "مدیریت امانت¬دادن کتاب¬ها ", IWantTo = "امانت¬دادن کتاب¬ها را ثبت کنم")]
        [Scenario("تحویل کتاب امانت داده شده بعد از تاریخ برگشت")]
        public class Successful : EFDataContextDatabaseFixture
        {
            private LendingService sut;
            private EFDataContext context;
            private Book book;
            private Member member;
            private UpdateLendingDto dto;
            Func<Task> expected;
            private Lending lending;
            public Successful(ConfigurationFixture configuration) : base(configuration)
            {
                context = CreateDataContext();
                var unitOfWork = new EFUnitOfWork(context);
                var repository = new EFLendingRepository(context);
                var memberRepository = new EFMemberRepository(context);
                var bookRepository = new EFBookRepository(context);

                sut = new LendingAppService(repository, unitOfWork, memberRepository, bookRepository);
            }
            [Given("تنها یک کتاب با عنوان شازده کوچولو به یک عضو کتابخانه" +
                " با نام علی قناعت پیشه با تاریخ برگشت 2021/07/02 به امانت" +
                " داده شده است")]
            [And("یک کتاب با عنوان شازده کوچولو  و با رده سنی 16 تا 80 سال" +
                " در فهرست کتاب¬ها وجود دارد")]
            private void Given()
            {
                var birthDate = new DateTime(2009, 07, 02);
                member = new MemberBuilder().WithFirstName("علی")
                                                .WithLastName("قناعت پیشه")
                                                .WithBirthDate(birthDate)
                                                .Build(context);
                var category = CategoryFactory.GenerateCategory(context, "dummy");
                book = new BookBuilder().WithTitle("شازده کوچولو")
                                            .WithMinAge(16)
                                            .WithMaxAge(80)
                                            .Build(context, category.Id);
                var returnDate = new DateTime(2021, 07, 02);
                lending = new LendingBuilder().WithReturnDate(returnDate)
                                                     .Build(context, book.Id, member.Id);
                dto = LendingFactory.GenerateUpdateLendingDto(returnDate, returnDate.AddDays(14), member.Id,book.Id);
               
            }
            [When(": کتاب با عنوان شازده کوچولو توسط عضو کتابخانه با نام" +
                " علی قناعت پیشه در تاریخ 2021/05/07 تحویل داده شود")]
            private void When()
            {
                expected = () =>  sut.UpdateDeliveryDate(lending.Id);
            }
            [Then("تنها یک امانت داده شده مربوط به کتاب با عنوان" +
                " شازده کوچولو به یک عضو کتابخانه با نام علی قناعت پیشه" +
                " با تاریخ برگشت 2021/07/02 و تاریخ تحویل 2021/07/05  وجود" +
                " داشته باشد")]
            [And("خطای تاریخ تحویل بعد از تاریخ برگشت می¬باشد نمایش داده شود")]
            private void Then()
            {
                
                expected.Should().ThrowExactly<DeliveryDateIsAfterReturnDateException>();
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
