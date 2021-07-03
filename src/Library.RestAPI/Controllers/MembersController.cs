using Library.Services.Members.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly MemberService _service;
        public MembersController(MemberService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add(AddMemberDto dto)
        {
            await _service.Add(dto);
        }
    }
}
