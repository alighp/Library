using Library.Entities;
using Library.Services.Books.Contracts;

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
    }
}
