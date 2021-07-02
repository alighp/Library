using System;
using System.Collections.Generic;
using System.Text;
using Library.Entities;
using Library.Services.Lendings.Contracts;

namespace Library.Persistence.Lendings
{
    public class EFLendingRepository : LendingRepository
    {
        private readonly EFDataContext _context;

        public EFLendingRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Lending lending)
        {
            _context.Lendings.Add(lending);
        }
    }
}
