using Library.Entities;
using Library.Services.Books.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<GetBookDto>> GetAllBooksByCategoryId(int categoryId)
        {
            var check =  await (from b in _context.Books
                          where b.CategoryId == categoryId
                          select new GetBookDto
                          {
                              Title = b.Title,
                              Author = b.Author,
                              MinAge = b.MinAge,
                              MaxAge = b.MaxAge,
                              CategoryId = b.CategoryId
                          }).ToListAsync();
            return check;
        }
    }
}
