using Library.Entities;
using Library.Persistence;

namespace Library.TestTools.Books
{
    public class BookBuilder
    {
        private Book book = new Book
        {
            Title = "dummy",
            Author = "dummy",
            MinAge = 10,
            MaxAge = 20,
            Category = new BookCategory { Title = "dummy-category" }
        };

        public BookBuilder WithAuthor(string author)
        {
            book.Author = author;
            return this;
        }

        public BookBuilder WithCategory(int categoryId)
        {
            book.CategoryId = categoryId;
            return this;
        }
        public BookBuilder WithMinAge(int categoryId)
        {
            book.CategoryId = categoryId;
            return this;
        }
        public BookBuilder WithMaxAge(int categoryId)
        {
            book.CategoryId = categoryId;
            return this;
        }

        public Book Build(EFDataContext context)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }
    }
}
