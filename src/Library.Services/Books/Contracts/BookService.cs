using System.Threading.Tasks;
using Library.Services.Categories;

namespace Library.Services.Books.Contracts
{
    public interface BookService
    {
        Task<int> Add(AddBookDto dto);
        Task Update(UpdateBookDto dto, int id);
    }
}
