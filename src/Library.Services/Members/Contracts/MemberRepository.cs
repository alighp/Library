using Library.Entities;

namespace Library.Services.Members.Contracts
{
    public interface MemberRepository
    {
        void Add(Member member);
        Member FindById(int memberId);
    }
}
