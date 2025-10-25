using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entities;
using TransferCenterDbStore.Interfaces;

namespace TransferCenterDbStore.Repositories;

public class PatientTransferInfoRepository: GenericRepository<PatientTransferInfo>, IPatientTransferInfoRepository
{
    public PatientTransferInfoRepository(BaseDbContext  dbContext) : base(dbContext)
    {
            
    }
}
