using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Categories.Contracts
{
    public interface BookCategoryService
    {
        Task<int> Add(AddBookCategoryDto dto);
    }
}
