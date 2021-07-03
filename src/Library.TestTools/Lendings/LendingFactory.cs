using Library.Entities;
using Library.Persistence;
using Library.Persistence.Books;
using Library.Persistence.Lendings;
using Library.Persistence.Members;
using Library.Services.Lendings;
using Library.Services.Lendings.Contracts;
using System;

namespace Library.TestTools.Lendings
{
    public static class LendingFactory
    {
        public static LendingAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFLendingRepository(context);
            var Bookepository = new EFBookRepository(context);
            var memberRepository = new EFMemberRepository(context);
            return new LendingAppService(repository, unitOfWork, memberRepository, Bookepository);
        }
        public static Lending GenerateLending(EFDataContext context, int memberId, int bookId, DateTime returnDate)
        {
            var lending = new Lending
            {
                ReturnDate = returnDate,
                MemberId = memberId,
                BookId = bookId
            };
            context.Lendings.Add(lending);
            context.SaveChanges();
            return lending;
        }
        public static AddLendingDto GenerateAddLendingDto(DateTime returnDate, int memberId, int bookId)
        {
            return new AddLendingDto
            {
                ReturnDate = returnDate,
                MemberId = memberId,
                BookId = bookId
            };
        }
        public static UpdateLendingDto GenerateUpdateLendingDto(DateTime returnDate, DateTime deliveryDate, int memberId, int bookId)
        {
            return new UpdateLendingDto
            {
                DeliveryDate = deliveryDate,
                ReturnDate = returnDate,
                MemberId = memberId,
                BookId = bookId
            };
        }
    }
}
