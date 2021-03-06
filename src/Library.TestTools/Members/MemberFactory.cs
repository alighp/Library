using Library.Entities;
using Library.Persistence;
using Library.Persistence.Members;
using Library.Services.Members;
using Library.Services.Members.Contracts;
using System;

namespace Library.TestTools.Members
{
    public static class MemberFactory
    {
        public static MemberAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFMemberRepository(context);
            return new MemberAppService(repository, unitOfWork);
        }
        public static Member GenerateMember(EFDataContext context)
        {
            var member = new Member
            {
                Address = "dummy",
                FirstName = "dummy",
                LastName = "dummy",
                BirthDate = new DateTime(1993, 07, 02)
            };
            context.Members.Add(member);
            context.SaveChanges();
            return member;
        }
        public static AddMemberDto GenerateAddMemberDto(DateTime birthDate, string address = "dummy", string firstName = "dummy", string lastName = "dummy")
        {
            return new AddMemberDto
            {
                Address = address,
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate
            };
        }
    }
}
