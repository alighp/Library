using System;

namespace Library.Services.Lendings.Contracts
{
    public class AddLendingDto
    {
        public DateTime ReturnDate { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
    }
}
