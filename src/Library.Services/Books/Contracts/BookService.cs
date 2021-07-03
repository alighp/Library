using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Books.Contracts
{
    public interface BookService
    {
        Task<int> Add(AddBookDto dto);
        Task Update(UpdateBookDto dto, int id);
        Task<List<GetBookDto>> GetAllBooksByCategoryId(int id);
    }
}
