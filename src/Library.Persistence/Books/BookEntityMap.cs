using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Library.Persistence.Books
{
    class BookEntityMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Title).IsRequired().IsUnicode().HasMaxLength(100);
            builder.Property(_ => _.Author).IsRequired().IsUnicode().HasMaxLength(100);
            builder.Property(_ => _.MaxAge).IsRequired();
            builder.Property(_ => _.MinAge).IsRequired();
            builder.Property(_ => _.CategoryId).IsRequired();
            builder.HasOne(_ => _.Category).WithMany().HasForeignKey(_ => _.CategoryId);
        }
    }
}
