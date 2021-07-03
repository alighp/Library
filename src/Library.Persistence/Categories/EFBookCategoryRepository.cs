using System.Linq;
using System.Threading.Tasks;
using Library.Entities;
using Library.Persistence;
using Library.Services.Books.Contracts;
using Library.Services.Categories;
using Library.Services.Categories.Contracts;

namespace Library.Persistence.Categories
{
    public class EFBookCategoryRepository: BookCategoryRepository
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
    }
}
