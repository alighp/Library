﻿using Library.Entities;
using Library.Services.Categories.Contracts;
using System.Threading.Tasks;

namespace Library.Services.Categories
{
    public class BookCategoryAppService : BookCategoryService
    {
        private readonly BookCategoryRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public BookCategoryAppService(BookCategoryRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(AddBookCategoryDto dto)
        {

            BookCategory bookCategory = MakeBookCategory(dto);
            _repository.Add(bookCategory);
            await _unitOfWork.Completed();
            return bookCategory.Id;
        }


        private BookCategory MakeBookCategory(AddBookCategoryDto dto)
        {
            return new BookCategory
            {
                Title = dto.Title
            };
        }
    }
}
