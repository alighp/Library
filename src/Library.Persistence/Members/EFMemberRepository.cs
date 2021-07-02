using Library.Entities;
using Library.Services.Members.Contracts;

namespace Library.Persistence.Members
{
    public class EFMemberRepository : MemberRepository
    {
        private readonly EFDataContext _context;

        public EFMemberRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Member member)
        {
            _context.Members.Add(member);
        }

        public Member FindById(int memberId)
        {
            return _context.Members.Find(memberId);
        }
    }
}
