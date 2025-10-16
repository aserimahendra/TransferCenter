using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entity;
using TransferCenterDbStore.Interface;

namespace TransferCenterDbStore.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
