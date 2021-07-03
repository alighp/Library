using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Lendings
{
    class LendingEntityMap : IEntityTypeConfiguration<Lending>
    {
        public void Configure(EntityTypeBuilder<Lending> builder)
        {
            builder.ToTable("Lendings");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.ReturnDate).IsRequired();
            builder.Property(_ => _.DeliveryDate);
            builder.Property(_ => _.MemberId).IsRequired();
            builder.Property(_ => _.BookId).IsRequired();
            builder.HasOne(_ => _.Book)
                   .WithMany()
                   .HasForeignKey(_ => _.BookId);
            builder.HasOne(_ => _.member)
                   .WithMany()
                   .HasForeignKey(_ => _.MemberId);
        }
    }
}
