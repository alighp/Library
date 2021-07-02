using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Members.Contracts
{
    public interface MemberService
    {
        Task<int> Add(AddMemberDto dto);
    }
}
