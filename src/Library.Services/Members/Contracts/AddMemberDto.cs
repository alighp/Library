using System;

namespace Library.Services.Members.Contracts
{
    public class AddMemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
    }
}
