using System.Threading.Tasks;
using Library.Entities;
using Library.Services.Books.Contracts;

namespace Library.Services.Books
{
    public class BookAppService : BookService
    {
        private readonly BookRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly BookCategoryRepository _categoryRepository;

        public BookAppService(BookRepository repository, UnitOfWork unitOfWork, BookCategoryRepository categoryRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }
        public async Task<int> Add(AddBookDto dto)
        {
            Book book = new Book {
                Author = dto.Author,
                MaxAge = dto.MaxAge,
                MinAge = dto.MinAge,
                Title = dto.Title,
                CategoryId = dto.CategoryId
            };
            _repository.Add(book);
            await _unitOfWork.Completed();
            return book.Id;
        }

        public async Task Update(UpdateBookDto dto, int id)
        {
            Book book = _repository.FindById(id);
            book.Title = dto.Title;
            book.Author = dto.Author;
            book.MinAge = dto.MinAge;
            book.MaxAge = dto.MaxAge;
            book.CategoryId = dto.CategoryId;
            await _unitOfWork.Completed();
        }
    }
}
