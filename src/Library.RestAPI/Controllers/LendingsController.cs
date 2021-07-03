using Library.Services.Lendings.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LendingsController : ControllerBase
    {
        private readonly LendingService _service;
        public LendingsController(LendingService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<int> Post(AddLendingDto dto)
        {
            return await _service.Add(dto);
        }

        [HttpPut("{id}")]
        public async Task Put(int id)
        {
            await _service.UpdateDeliveryDate(id);

        }


    }
}
