using Library.Entities;
using System.Collections.Generic;

namespace Library.Services.Books.Contracts
{
    public interface BookRepository
    {
        void Add(Book book);
        Book FindById(int id);
        List<GetBookDto> GetAllBooksByCategoryId(int id);
    }
}
