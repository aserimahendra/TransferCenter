using TransferCenterDbStore.Data;

namespace TransferCenterDbStore.Interfaces;

public interface IDbContextFactory
{
    BaseDbContext CreateDbContext();
}