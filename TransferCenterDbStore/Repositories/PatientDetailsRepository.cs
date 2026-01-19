using TransferCenterDbStore.Data;
using TransferCenterDbStore.Entities;
using TransferCenterDbStore.Interfaces;

namespace TransferCenterDbStore.Repositories;

public class PatientDetailsRepository: GenericRepository<PatientDetails>, IPatientDetailsRepository
{
    public PatientDetailsRepository(BaseDbContext dbContext): base(dbContext)
    {
            
    }
}