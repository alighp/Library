using Library.Entities;
using Library.Persistence;
using System;

namespace Library.TestTools.Lendings
{
    public class LendingBuilder
    {
        private Lending lending = new Lending
        {

            ReturnDate = new DateTime(2021, 07, 02),
        };
        public LendingBuilder WithReturnDate(DateTime date)
        {
            lending.ReturnDate = date;
            return this;
        }
        public LendingBuilder WithDeliveryDate(DateTime date)
        {
            lending.DeliveryDate = date;
            return this;
        }
        public Lending Build(EFDataContext context, int bookId, int memberId)
        {
            lending.BookId = bookId;
            lending.MemberId = memberId;
            context.Lendings.Add(lending);
            context.SaveChanges();
            return lending;
        }
    }
}
