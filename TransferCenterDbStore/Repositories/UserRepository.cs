using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entities;
using TransferCenterDbStore.Interfaces;

namespace TransferCenterDbStore.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }
}