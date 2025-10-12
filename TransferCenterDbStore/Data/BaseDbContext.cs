using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferCenterDbStore.Entity;

namespace TransferCenterDbStore.Data
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }


        public DbSet<tblUser> tblUser { get; set; }
    }
}
