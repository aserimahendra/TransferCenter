using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entity;

namespace TransferCenterDbStore.Repository
{
    public class UserRepository : GenericRepository<tblUser>
    {
        public UserRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
