using System.Threading.Tasks;
using Library.Entities;
using Library.Services.Categories;

namespace Library.Services.Categories.Contracts
{
    public interface BookCategoryRepository
    {
        void Add(BookCategory bookCategory);
        bool ExistById(int categoryId);
    }
}
