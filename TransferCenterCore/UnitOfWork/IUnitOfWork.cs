using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferCenterCore.Repository;

namespace TransferCenterCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();

        //Define the Specific Repositories
        UserRepository UserRepository { get; }
    }
}
