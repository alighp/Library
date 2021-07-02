using Library.Services;
using System.Threading.Tasks;

namespace Library.Persistence
{
    public class EFUnitOfWork : UnitOfWork
    {
        private readonly EFDataContext Context;
        public EFUnitOfWork(EFDataContext context)
        {
            Context = context;
        }
        public async Task Completed()
        {
            await Context.SaveChangesAsync();
        }
    }
}
