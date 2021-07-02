﻿using Library.Services.Members.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Add(AddMemberDto dto)
        {
            _service.Add(dto);
        }
    }
}
