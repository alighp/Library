using System.Threading.Tasks;
using Library.Entites;
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
            _context.bookCategories.Add(category);

        }
    }
}
