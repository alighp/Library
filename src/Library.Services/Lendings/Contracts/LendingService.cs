using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Lendings.Contracts
{
    public interface LendingService
    {
        Task<int> Add(AddLendingDto dto);
        Task UpdateDeliveryDate(int id);
    }
}
