using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Entities;
using Library.Persistence;
using Library.Services.Books.Contracts;
using Library.Services.Categories;
using Library.Services.Tests.Specs.Infrastructure;
using Library.TestTools.Books;
using Library.TestTools.Categories;
using Xunit;

namespace Library.Services.Tests.Specs.Books.Add
{
    [Scenario("ثبت یک کتاب در دسته¬بندی")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly BookService sut;
        private BookCategory bookCategory;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = BookFactory.CreateService(context);
        }
        [Given("یک دسته¬بندی با عنوان رمان خارجی وجود دارد")]
        private void Given() {

            bookCategory = CategoryFactory.GenerateCategory(context, "رمان خارجی");
        }
        [When("کتابی با عنوان شازده کوچولو و نویسنده¬ آنتوان دوسنت اگزوپری " +
            "در دسته¬بندی رمان خارجی و با رده سنی 16تا80 سال اضافه می¬نمایم")]
        private async Task When()
        {
            var dto = BookFactory.GenerateAddBookDto(bookCategory.Id,"شازده کوچولو",
                "آنتوان دوسنت اگزوپری",16,80);
            await sut.Add(dto);
        }
        [Then("باید تنها یک دسته¬بندی با عنوان رمان خارجی" +
            " در فهرست دسته¬بندی کتاب¬ها وجود داشته باشد")]
        private void Then()
        {

            var expected = context.Books.First();
            expected.Author.Should().Be("آنتوان دوسنت اگزوپری");
            expected.Title.Should().Be("شازده کوچولو");
            expected.MaxAge.Should().Be(80);
            expected.MinAge.Should().Be(16);
            expected.CategoryId.Should().Be(bookCategory.Id);
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
