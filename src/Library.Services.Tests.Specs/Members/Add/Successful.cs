﻿using FluentAssertions;
using Library.Persistence;
using Library.Services.Members.Contracts;
using Library.Services.Tests.Specs.Infrastructure;
using Library.TestTools.Members;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Specs.Members.Add
{
    [Feature(title: "", AsA = "من به عنوان کتابدار", InOrderTo = "مدیریت اعضای کتابخانه", IWantTo = "مشخصات اعضا را ثبت کنم")]
    [Scenario("ثبت یک عضو جدید به کتابخانه")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private MemberService sut;
        private EFDataContext context;
        private AddMemberDto dto;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = MemberFactory.CreateService(context);
        }
        [Given("هیچ عضوی در کتابخانه وجود ندارد")]
        private void Given()
        {

        }
        [When("یک شخص با نام علی قناعت پیشه و تاریخ تولد 1993/07/02 و آدرس " +
            "صدرا – فازیک – بلوار فردوسی به فهرست اعضای کتابخانه اضافه می کنم.")]
        private async Task When()
        {
            var birthDate = new DateTime(1993, 07, 02);
            dto = MemberFactory.GenerateAddMemberDto(birthDate, "صدرا – فازیک – بلوار فردوسی", "علی", "قناعت پیشه");
            await sut.Add(dto);
        }
        [Then("باید تنها یک شخص با نام علی قناعت پیشه و تاریخ تولد 1993/07/02 و آدرس " +
            "صدرا – فازیک – بلوار فردوسی در فهرست اعضای کتابخانه وجود داشته باشد")]
        private void Then()
        {

            var expected = context.Members.First();
            expected.Address.Should().Be(dto.Address);
            expected.FirstName.Should().Be(dto.FirstName);
            expected.LastName.Should().Be(dto.LastName);
            expected.BirthDate.Should().Be(dto.BirthDate);
        }
        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then()
                );
        }
    }
}
