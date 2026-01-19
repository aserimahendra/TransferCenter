using TransferCenterCore.Models;

namespace TransferCenterCore.Interfaces;

public interface IAuditLogService
{
        public Task SaveAsync(AuditLog auditLog);
        public void Save(AuditLog auditLog);
}
