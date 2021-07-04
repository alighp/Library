using Library.Services.Books.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _service;
        public BooksController(BookService service)
        {
            _service = service;
        }
        [HttpGet("{categoryId}")]
        public async Task<List<GetBookDto>> GetAllBooksByCategoryId(int categoryId)
        {
            return await _service.GetAllBooksByCategoryId(categoryId);
        }
        [HttpPost]
        public async Task<int> Post(AddBookDto dto)
        {
            return await _service.Add(dto);
        }
        [HttpPut("{id}")]
        public async Task Put(int id, UpdateBookDto dto)
        {
            await _service.Update(dto, id);

        }
    }
}
