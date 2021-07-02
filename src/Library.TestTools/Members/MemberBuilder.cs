using Library.Entities;
using Library.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TestTools.Members
{
    public class MemberBuilder
    {
        private Member member = new Member{

            Address = "dummy",
            FirstName = "dummy",
            LastName = "dummy",
            BirthDate = new DateTime(1993, 07, 02)
        };
        public MemberBuilder WithAddress(string address) {
            member.Address = address;
            return this;
        }
        public MemberBuilder WithFirstName(string firstName)
        {
            member.FirstName = firstName;
            return this;
        }
        public MemberBuilder WithLastName(string lastName)
        {
            member.LastName = lastName;
            return this;
        }
        public MemberBuilder WithBirthDate(DateTime birthDate)
        {
            member.BirthDate = birthDate;
            return this;
        }
        public Member Build(EFDataContext context) {
            context.Members.Add(member);
            context.SaveChanges();
            return member;
        } 
    }
}
