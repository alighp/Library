using Library.Services.Books.Contracts;
using Library.Services.Lendings.Contracts;
using Library.Services.Members.Contracts;

namespace Library.Services.Lendings
{
    public class LendingAppservice : LendingService
    {
        private readonly LendingRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly MemberRepository _memberRepository;
        private readonly BookRepository _bookRepository;


        public LendingAppservice(LendingRepository repository, UnitOfWork unitOfWork, MemberRepository memberRepository, BookRepository bookRepository)
        {
            _memberRepository = memberRepository;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }
    }
}
