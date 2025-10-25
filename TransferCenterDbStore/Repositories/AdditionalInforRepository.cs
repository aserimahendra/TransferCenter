using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entities;
using TransferCenterDbStore.Interfaces;


namespace TransferCenterDbStore.Repositories;

public class AdditionalInforRepository: GenericRepository<AdditionalInfo>, IAdditionalInfoRepository
{
    public AdditionalInforRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }
}