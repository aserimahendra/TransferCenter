using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferCenterCore.Data;
using TransferCenterModel;

namespace TransferCenterCore.Repository
{
    public class UserRepository : GenericRepository<tblUser>
    {
        public UserRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
