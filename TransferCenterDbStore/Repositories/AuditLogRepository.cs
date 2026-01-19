using System;
using TransferCenterDbStore.Data;

namespace TransferCenterDbStore.Repositories;

public class AuditLogRepository : GenericRepository<Entities.AuditLog>, Interfaces.IAuditLogRepository
{
    public AuditLogRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }
}
