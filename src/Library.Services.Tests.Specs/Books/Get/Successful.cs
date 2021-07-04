using FluentAssertions;
using Library.Entities;
using Library.Persistence;
using Library.Services.Books.Contracts;
using Library.Services.Tests.Specs.Infrastructure;
using Library.TestTools.Books;
using Library.TestTools.Categories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Specs.Books.Get
{
    [Feature(title: "", AsA = "من به عنوان کتابدار", InOrderTo = "مدیریت کتابها  ", IWantTo = "فهرست کتاب ها را نمایش دهم.")]
    [Scenario("نمایش فهرست کتاب¬های یک دسته¬بندی خاص")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly BookService sut;
        private BookCategory bookCategory;
        private List<GetBookDto> expected;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = BookFactory.CreateService(context);
        }

        [Given("د تنها یک کتاب با عنوان شازده کوچولو و نویسنده" +
            "آنتوان دوسنت اگزوپری در دسته¬بندی رمان خارجی و با رده سنی 16 تا 80 وجود دارد")]
        private void Given()
        {
            bookCategory = CategoryFactory.GenerateCategory(context, "رمان خارجی");
            var book = new BookBuilder().WithTitle("شازده کوچولو")
                                        .WithAuthor("آنتوان دوسنت اگزوپری")
                                        .WithMinAge(16)
                                        .WithMaxAge(80)
                                        .withCategoryId(bookCategory.Id)
                                        .Build(context);
        }
        [When("فهرست کتاب¬های دسته¬بندی رمان خارجی را مشاهده می¬کنم")]
        private async Task When()
        {
            expected = await sut.GetAllBooksByCategoryId(bookCategory.Id);
        }
        [Then("تنها یک کتاب با عنوان شازده کوچولو و نویسنده آنتوان دوسنت اگزوپری " +
            "در دسته¬بندی رمان خارجی و با رده سنی 16 تا 80 در فهرست کتاب¬های دسته¬بندی" +
            " رمان خارجی باید وجود داشته باشد")]
        private void Then()
        {
            expected.First().Author.Should().Be("آنتوان دوسنت اگزوپری");
            expected.First().Title.Should().Be("شازده کوچولو");
            expected.First().MinAge.Should().Be(16);
            expected.First().MaxAge.Should().Be(80);
            expected.First().CategoryId.Should().Be(bookCategory.Id);
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
