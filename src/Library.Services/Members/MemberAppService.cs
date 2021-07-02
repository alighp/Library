using Library.Entities;
using Library.Services.Members.Contracts;
using System.Threading.Tasks;

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

        public async Task<int> Add(AddMemberDto dto)
        {
            Member member = new Member { 
                Address = dto.Address,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate
            };
            _repository.Add(member);
            await _unitOfWork.Completed();
            return member.Id;
        }
    }
}
