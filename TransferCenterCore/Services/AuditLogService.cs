using TransferCenterCore.Interfaces;
using TransferCenterCore.Models;
using TransferCenterCore.Translators;
using TransferCenterDbStore.UnitOfWork;

namespace TransferCenterCore.Services;

public class AuditLogService : IAuditLogService
{
    IUnitOfWork _unitOfWork;
    public AuditLogService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void Save(AuditLog auditLog)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(AuditLog auditLog)
    {
        try
        {
            auditLog.UserId = "gjgj";
            await _unitOfWork.AuditLogRepository.AddAsync(auditLog.ToEntity());
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
