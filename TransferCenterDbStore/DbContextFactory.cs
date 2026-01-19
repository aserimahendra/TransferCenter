using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TransferCenterDbStore.Data;
using TransferCenterDbStore.Interfaces;

namespace TransferCenterDbStore;

public class DbContextFactory : IDbContextFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DbContextFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public BaseDbContext CreateDbContext()
    {
        var scope = _serviceScopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<BaseDbContext>();
    }
}