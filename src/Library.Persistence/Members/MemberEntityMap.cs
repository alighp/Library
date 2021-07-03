using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Members
{
    class MemberEntityMap : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Address).IsRequired().IsUnicode().HasMaxLength(100);
            builder.Property(_ => _.BirthDate).IsRequired();
            builder.Property(_ => _.FirstName).IsRequired().IsUnicode().HasMaxLength(50);
            builder.Property(_ => _.LastName).IsRequired().IsUnicode().HasMaxLength(50);
        }
    }
}
