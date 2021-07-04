using FluentAssertions;
using Library.Entities;
using Library.Persistence;
using Library.Services.Books.Contracts;
using Library.Services.Tests.Specs.Infrastructure;
using Library.TestTools.Books;
using Library.TestTools.Categories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Specs.Books.Update
{
    [Feature(title: "", AsA = "من به عنوان کتابدار", InOrderTo = "مدیریت کتابها  ", IWantTo = "کتاب ویرایش کنم")]
    [Scenario("ویرایش مشخصات کتاب")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly BookService sut;
        private BookCategory bookCategory;
        private Book book;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = BookFactory.CreateService(context);
        }
        [Given("تنها یک کتاب با عنوان شازده کوچولو و نویسنده¬ آنتوان دوسنت اگزوپری" +
            "در دسته¬بندی رمان خارجی و با رده سنی 16تا80 سال در فهرست کتاب¬ها وجود دارد.")]
        private void Given()
        {

            bookCategory = CategoryFactory.GenerateCategory(context, "رمان خارجی");
            book = new BookBuilder().WithAuthor("شازده کوچولو")
                                    .WithAuthor("آنتوان دوسنت اگزوپری")
                                    .WithMinAge(16)
                                    .WithMaxAge(80)
                                    .Build(context, bookCategory.Id);
        }
        [When("عنوان کتاب را به گیله¬مرد ویرایش میکنم")]
        private async Task When()
        {
            var dto = BookFactory.GenerateUpdateBookDto(bookCategory.Id, "گیله¬مرد","بزرگ علوی", 16, 80);
            await sut.Update(dto, book.Id);
        }
        [Then("باید در فهرست کتاب¬ها، تنها یک کتاب با عنوان گیله¬مرد و نویسنده¬ آنتوان دوسنت اگزوپری در دسته¬بندی رمان خارجی و با رده سنی 16تا80 سال وجود داشته باشد")]
        private void Then()
        {

            var expected = context.Books.First();
            expected.Author.Should().Be("بزرگ علوی");
            expected.Title.Should().Be("گیله¬مرد");
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
