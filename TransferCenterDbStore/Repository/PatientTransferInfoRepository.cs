using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entity;
using TransferCenterDbStore.Interface;

namespace TransferCenterDbStore.Repository;

public class PatientTransferInfoRepository: GenericRepository<PatientTransferInfo>, IPatientTransferInfoRepository
{
    public PatientTransferInfoRepository(BaseDbContext  dbContext) : base(dbContext)
    {
            
    }
}
