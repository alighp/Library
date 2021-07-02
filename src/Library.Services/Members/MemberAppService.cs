using Library.Services.Members.Contracts;

namespace Library.Services.Members
{
    public class MemberAppService : MemberService
    {
        private readonly MemberRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public MemberAppService(MemberRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
    }
}
