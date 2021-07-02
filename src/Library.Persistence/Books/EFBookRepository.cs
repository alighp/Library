using Library.Entities;
using Library.Services.Books.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Library.Persistence.Books
{
    public class EFBookRepository : BookRepository
    {
        private readonly EFDataContext _context;

        public EFBookRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public Book FindById(int id)
        {
            return _context.Books.Find(id);
        }

        public List<GetBookDto> GetAllBooksByCategoryId(int categoryId)
        {
            return (from b in _context.Books
                    join bc in _context.BookCategories
                    on b.CategoryId equals bc.Id
                    select new GetBookDto
                    {
                        Title = b.Title,
                        Author = b.Author,
                        MinAge = b.MinAge,
                        MaxAge = b.MaxAge,
                        CategoryId = b.CategoryId
                    }).ToList();
        }
    }
}
