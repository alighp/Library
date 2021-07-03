using Library.Entities;
using Library.Persistence;
using Library.Persistence.Books;
using Library.Persistence.Categories;
using Library.Services.Books;
using Library.Services.Books.Contracts;

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
        public static Book GenerateBook(EFDataContext context)
        {
            var book = new Book
            {
                Title = "dummy",
                Author = "dummy",
                MinAge = 10,
                MaxAge = 20,
                Category = new BookCategory { Title = "dummy-category-title" }
            };
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }
        public static AddBookDto GenerateAddBookDto(int categoryId, string title = "dummy", string author = "dummy", byte minAge = 10, byte maxAge = 20)
        {
            return new AddBookDto
            {
                Title = title,
                Author = author,
                MinAge = minAge,
                MaxAge = maxAge,
                CategoryId = categoryId
            };
        }
        public static UpdateBookDto GenerateUpdateBookDto(int categoryId, string title = "dummy", string author = "dummy", byte minAge = 10, byte maxAge = 20)
        {
            return new UpdateBookDto
            {
                Title = title,
                Author = author,
                MinAge = minAge,
                MaxAge = maxAge,
                CategoryId = categoryId
            };
        }
    }
}
