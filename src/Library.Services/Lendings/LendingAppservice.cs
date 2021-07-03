using Library.Entities;
using Library.Services.Books.Contracts;
using Library.Services.Books.Exceptions;
using Library.Services.Lendings.Contracts;
using Library.Services.Lendings.Exceptions;
using Library.Services.Members.Contracts;
using Library.Services.Members.Exceptions;
using System;
using System.Threading.Tasks;

namespace Library.Services.Lendings
{
    public class LendingAppService : LendingService
    {
        private readonly LendingRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly MemberRepository _memberRepository;
        private readonly BookRepository _bookRepository;


        public LendingAppService(LendingRepository repository, UnitOfWork unitOfWork, MemberRepository memberRepository, BookRepository bookRepository)
        {
            _memberRepository = memberRepository;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        public async Task<int> Add(AddLendingDto dto)
        {
            GaurdAgainstBookNotfound(dto.BookId);
            GaurdAgainstMemberNotfound(dto.MemberId);
            GaurdAgainstMemberAgeCantBeOutOfBookAgeRange(dto);
            Lending lending = new Lending
            {
                ReturnDate = dto.ReturnDate,
                BookId = dto.BookId,
                MemberId = dto.MemberId
            };
            _repository.Add(lending);
            await _unitOfWork.Completed();
            return lending.Id;
        }


        public async Task UpdateDeliveryDate(int LendingId)
        {
            GaurdAgainstLendingNotfound(LendingId);
            Lending lending = _repository.Find(LendingId);
            lending.DeliveryDate = DateTime.UtcNow;
            await _unitOfWork.Completed();
            GaurdAgainstDeliveryDateAfterRetrunDate(lending.ReturnDate);
            await _unitOfWork.Completed();
        }

        private void GaurdAgainstLendingNotfound(int lendingId)
        {
            if (_repository.Find(lendingId)==null)
                throw new LendingNotFoundException();
        }

        private void GaurdAgainstMemberNotfound(int memberId)
        {
            if (_memberRepository.FindById(memberId) == null)
                throw new MemberNotFoundException();
        }

        private void GaurdAgainstBookNotfound(int bookId)
        {
            if (_bookRepository.FindById(bookId) == null)
                throw new BookNotFoundException();
        }

        private static void GaurdAgainstDeliveryDateAfterRetrunDate(DateTime returnDate)
        {

            if (DateTime.UtcNow > returnDate)
                throw new DeliveryDateIsAfterReturnDateException();
        }

        private void GaurdAgainstMemberAgeCantBeOutOfBookAgeRange(AddLendingDto dto)
        {
            var book = _bookRepository.FindById(dto.BookId);
            var member = _memberRepository.FindById(dto.MemberId);
            var memberAge = (byte)((DateTime.UtcNow - member.BirthDate).TotalDays / 365);
            if (memberAge < book.MinAge || memberAge > book.MaxAge)
                throw new MemberAgeOutOfBookAgeRangeException();
        }
    }
}
