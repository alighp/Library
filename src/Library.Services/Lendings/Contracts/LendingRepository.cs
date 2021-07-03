using Library.Entities;

namespace Library.Services.Lendings.Contracts
{
    public interface LendingRepository
    {
        void Add(Lending lending);
        Lending Find(int id);
    }
}
