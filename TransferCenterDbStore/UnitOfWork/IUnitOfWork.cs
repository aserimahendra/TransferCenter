using TransferCenterDbStore.Repository;

namespace TransferCenterDbStore.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();

        //Define the Specific Repositories
        UserRepository UserRepository { get; }
    }
}
