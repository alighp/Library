using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Lendings.Contracts
{
    public interface LendingRepository
    {
        void Add(Lending lending);
    }
}
