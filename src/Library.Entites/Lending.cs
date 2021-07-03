using System;

namespace Library.Entities
{
    public class Lending
    {
        public int Id { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public Member member { get; set; }
    }
}
