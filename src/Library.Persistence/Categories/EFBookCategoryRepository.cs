using Library.Entities;
using Library.Services.Categories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<bool> ExistByTitle(string title)
        {
            return await _context.BookCategories.AnyAsync(_=>_.Title == title);
        }
    }
}
