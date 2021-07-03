using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Persistence;
using Library.Persistence.Categories;
using Library.Services.Categories;
using Library.Services.Categories.Contracts;
using Library.Services.Tests.Specs.Infrastructure;
using Library.TestTools.Categories;
using Xunit;

namespace Library.Services.Tests.Specs.Categories.Add
{

    [Feature(title: "", AsA = "من به عنوان کتابدار", InOrderTo = "مدیریت دسته¬بندی کتابها ", IWantTo = "دسته¬بندی ثبت کنم")]
    [Scenario("ثبت یک دسته بندی")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly BookCategoryService sut;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFBookCategoryRepository(context);
            sut = new BookCategoryAppService(repository, unitOfWork);

        }
        [Given("هیچ دسته¬بندی¬ای وجود ندارد.")]
        private void Given() { }
        [When("یک دسته بندی با عنوان رمان خارجی اضافه می کنم")]
        private async Task When()
        {
            var dto = CategoryFactory.GenerateAddBookCategoryDto(context, "رمان خارجی");
            await sut.Add(dto);
        }
        [Then("باید تنها یک دسته¬بندی با عنوان رمان خارجی" +
            " در فهرست دسته¬بندی کتاب¬ها وجود داشته باشد")]
        private void Then() {

            var expected = context.BookCategories.First();
            expected.Title.Should().Be("رمان خارجی");
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
