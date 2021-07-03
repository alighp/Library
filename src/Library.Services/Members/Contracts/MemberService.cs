using System.Threading.Tasks;

namespace Library.Services.Members.Contracts
{
    public interface MemberService
    {
        Task<int> Add(AddMemberDto dto);
    }
}
