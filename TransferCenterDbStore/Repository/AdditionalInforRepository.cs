using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entity;
using TransferCenterDbStore.Interface;

namespace TransferCenterDbStore.Repository;

public class AdditionalInforRepository: GenericRepository<AdditionalInfo>, IAdditionalInfoRepository
{
    public AdditionalInforRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }
}