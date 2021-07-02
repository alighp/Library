using Library.Entities;
using Library.Services.Books.Contracts;
using Library.Services.Lendings.Contracts;
using Library.Services.Lendings.Exceptions;
using Library.Services.Members.Contracts;
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

        private void GaurdAgainstMemberAgeCantBeOutOfBookAgeRange(AddLendingDto dto)
        {
            var book = _bookRepository.FindById(dto.BookId);
            var member = _memberRepository.FindById(dto.MemberId);
            var memberAge = (byte)(DateTime.Now.Subtract(member.BirthDate).Days / 365);
            if (memberAge < book.MinAge || memberAge > book.MaxAge)
                throw new MemberAgeOutOfBookAgeRangeException();
        }
    }
}
