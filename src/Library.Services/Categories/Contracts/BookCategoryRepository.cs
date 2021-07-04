using Library.Entities;
using System.Threading.Tasks;

namespace Library.Services.Categories.Contracts
{
    public interface BookCategoryRepository
    {
        void Add(BookCategory bookCategory);
        bool ExistById(int categoryId);
        Task<bool> ExistByTitle(string title);
    }
}
