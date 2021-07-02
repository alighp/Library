using System;
using System.Collections.Generic;
using System.Text;
using Library.Persistence;
using Library.Persistence.Books;
using Library.Persistence.Categories;
using Library.Services.Books;

namespace Library.TestTools.Books
{
    public static class BookFactory
    {
        public static BookAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFBookRepository(context);
            var categoryRepository = new EFBookCategoryRepository(context);
            return new BookAppService(repository, unitOfWork, categoryRepository);
        }
    }
}
