﻿using Library.Entities;
using Library.Persistence;
using Library.Persistence.Categories;
using Library.Services.Categories;
using Library.Services.Categories.Contracts;

namespace Library.TestTools.Categories
{
    public static class CategoryFactory
    {
        public static BookCategoryAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFBookCategoryRepository(context);
            return new BookCategoryAppService(repository, unitOfWork);
        }
        public static BookCategory GenerateCategory(EFDataContext context, string title = "dummy")
        {
            var category = new BookCategory
            {
                Title = title
            };
            context.BookCategories.Add(category);
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
