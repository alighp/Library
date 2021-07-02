using System.Threading.Tasks;
using Library.Entites;
using Library.Services.Categories;

namespace Library.Services.Books.Contracts
{
    public interface BookCategoryRepository
    {
        void Add(BookCategory bookCategory);
    }
}
