using Library.Entities;
using Library.Persistence;
using Library.Services.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TestTools.Categories
{
    public static class CategoryFactory
    {
        public static BookCategory GenerateCategory(EFDataContext context, string title="dummy") {
            var category = new BookCategory {
                Title = title
            };
            context.bookCategories.Add(category);
            context.SaveChanges();
            return category;
        }
        public static AddBookCategoryDto GenerateAddBookCategoryDto(EFDataContext context, string title = "dummy")
        {
            var category = new AddBookCategoryDto
            {
                Title = title
            };
            return category;
        }
    }
}
