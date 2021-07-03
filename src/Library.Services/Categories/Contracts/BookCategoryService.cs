using System.Threading.Tasks;

namespace Library.Services.Categories.Contracts
{
    public interface BookCategoryService
    {
        Task<int> Add(AddBookCategoryDto dto);
    }
}
