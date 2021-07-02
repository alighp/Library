using System;

namespace Library.Services.Lendings.Contracts
{
    public class UpdateLendingDto {
        public DateTime ReturnDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
    }
}
