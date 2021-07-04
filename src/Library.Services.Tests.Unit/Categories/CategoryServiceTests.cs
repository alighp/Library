using FluentAssertions;
using Library.Infrastructure.Test;
using Library.Persistence;
using Library.Services.Categories.Contracts;
using Library.Services.Categories.Exceptions;
using Library.TestTools.Categories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.Categories
{
    public class CategoryServiceTests
    {
        private readonly EFDataContext context;
        private readonly BookCategoryService sut;

        public CategoryServiceTests()
        {
            var db = new EFInMemoryDatabase();
            context = db.CreateDataContext<EFDataContext>();
            sut = CategoryFactory.CreateService(context);
        }

        [Fact]
        public void Add_throw_exception_when_category_title_exists()
        {
            CategoryFactory.GenerateCategory(context);
            var category = CategoryFactory.GenerateAddBookCategoryDto(context);
            Func<Task> expected = () => sut.Add(category);

            expected.Should().ThrowExactly<DuplicateCategoryTitleException>();
        }
    }
}
