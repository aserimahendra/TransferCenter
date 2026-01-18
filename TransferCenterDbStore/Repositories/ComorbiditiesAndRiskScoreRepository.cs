using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entities;
using TransferCenterDbStore.Interfaces;

namespace TransferCenterDbStore.Repositories;

public class ComorbiditiesAndRiskScoreRepository : GenericRepository<ComorbiditiesAndRiskScore>, IComorbiditiesAndRiskScoreRepository
{
    public ComorbiditiesAndRiskScoreRepository(BaseDbContext context) : base(context)
    {
    }

    // Add any specific methods for ComorbiditiesAndRiskScore if needed
}