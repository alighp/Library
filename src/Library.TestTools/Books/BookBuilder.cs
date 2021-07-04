﻿using Library.Entities;
using Library.Persistence;

namespace Library.TestTools.Books
{
    public class BookBuilder
    {
        private Book book = new Book
        {
            Title = "dummy",
            Author = "dummy",
            MinAge = 10,
            MaxAge = 20,
            Category = new BookCategory { Title = "dummy"}
        };

        public BookBuilder WithTitle(string title)
        {
            book.Title = title;
            return this;
        }
        public BookBuilder WithAuthor(string author)
        {
            book.Author = author;
            return this;
        }
        public BookBuilder WithMinAge(byte minAge)
        {
            book.MinAge = minAge;
            return this;
        }
        public BookBuilder withCategoryId(int categoryId)
        {
            book.Category = null;
            book.CategoryId = categoryId;
            return this;
        }
        public BookBuilder WithMaxAge(byte maxAge)
        {
            book.MaxAge = maxAge;
            return this;
        }

        public Book Build(EFDataContext context)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }
    }
}
