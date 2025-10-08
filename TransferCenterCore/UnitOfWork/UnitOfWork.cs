using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferCenterCore.Data;
using TransferCenterCore.Repository;

namespace TransferCenterCore.UnitOfWork
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

        UserRepository _userRepository;
      

        public UserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_dbContext); }
        }

       
        #endregion
    }
}
