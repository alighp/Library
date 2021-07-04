using Library.Entities;

namespace Library.Services.Categories.Contracts
{
    public interface BookCategoryRepository
    {
        void Add(BookCategory bookCategory);
        bool ExistById(int categoryId);
        bool ExistByTitle(string title);
    }
}
