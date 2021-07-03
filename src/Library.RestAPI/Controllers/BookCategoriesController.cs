using Library.Services.Categories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategoriesController : ControllerBase
    {
        private readonly BookCategoryService _service;
        public BookCategoriesController(BookCategoryService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add(AddBookCategoryDto dto)
        {
            await _service.Add(dto);
        }
    }
}
