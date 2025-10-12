using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entity;
using TransferCenterDbStore.Interface;

namespace TransferCenterDbStore.Repository;

public class PatientDetailsRepository: GenericRepository<PatientDetails>, IPatientDetailsRepository
{
    public PatientDetailsRepository(BaseDbContext dbContext): base(dbContext)
    {
            
    }
}