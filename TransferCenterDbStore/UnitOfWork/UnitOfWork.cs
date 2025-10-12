using TransferCenterDbStore.Data;
using TransferCenterDbStore.Interface;
using TransferCenterDbStore.Repository;

namespace TransferCenterDbStore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BaseDbContext _dbContext;
        public UnitOfWork(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Commit()
           => _dbContext.SaveChanges();

        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();

        public void Rollback()
            => _dbContext.Dispose();

        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();

        #region Properties

        IUserRepository _userRepository;
        IAdditionalInfoRepository _additionalInfoRepository;  
        IPatientDetailsRepository _patientDetailsRepository;
        IPatientTransferInfoRepository _patientTransferInfoRepository;

        public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_dbContext); }
        }
        
        public IAdditionalInfoRepository AdditionalInfoRepository
        {
            get { return _additionalInfoRepository = _additionalInfoRepository ?? new AdditionalInforRepository(_dbContext); }
        }
        public IPatientDetailsRepository PatientDetailsRepository
        {
            get { return _patientDetailsRepository ?? new PatientDetailsRepository(_dbContext); }
        }
        public IPatientTransferInfoRepository PatientTransferInfoRepository
        {
            get { return _patientTransferInfoRepository ?? new PatientTransferInfoRepository(_dbContext); }
        }
       
        #endregion
    }
}
