using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Library.Persistence
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions option) : base(option)
        {
        }
        public EFDataContext(string connectionString)
         : this(new DbContextOptionsBuilder<EFDataContext>().UseSqlServer(connectionString).Options)
        {
        }

        private EFDataContext(DbContextOptions<EFDataContext> options)
           : this((DbContextOptions)options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Lending> Lendings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
