using Library.Entities;
using Library.Services.Categories.Contracts;
using System.Linq;

namespace Library.Persistence.Categories
{
    public class EFBookCategoryRepository : BookCategoryRepository
    {
        private EFDataContext _context;

        public EFBookCategoryRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(BookCategory category)
        {
            _context.BookCategories.Add(category);
        }
        public bool ExistById(int categoryId)
        {
            return _context.BookCategories.Any(_ => _.Id == categoryId);
        }

        public bool ExistByTitle(string title)
        {
            return _context.BookCategories.Any(_=>_.Title == title);
        }
    }
}
