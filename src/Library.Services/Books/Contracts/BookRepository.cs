﻿using Library.Entites;

namespace Library.Services.Books.Contracts
{
    public interface BookRepository
    {
        void Add(Book book);
        //Book FindById(int id);
    }
}
