using TransferCenterDbStore.Interfaces;

namespace TransferCenterDbStore.UnitOfWork;

public interface IUnitOfWork
{
    void Commit();
    void Rollback();
    Task CommitAsync();
    Task RollbackAsync();

    //Define the Specific Repositories
    IUserRepository UserRepository { get; }
    IAdditionalInfoRepository  AdditionalInfoRepository { get; }
    IPatientDetailsRepository  PatientDetailsRepository { get; }
    IPatientTransferInfoRepository PatientTransferInfoRepository { get; }
    IAuditLogRepository AuditLogRepository { get; }
    IComorbiditiesAndRiskScoreRepository ComorbiditiesAndRiskScoreRepository { get; }

}