namespace TransferCenterCore.Translators;

public static class AuditLogTranslator
{
    // Web to Store Entity
    public static TransferCenterDbStore.Entities.AuditLog ToEntity(this Models.AuditLog source)
    {
        if (source == null) return null!;

        return new TransferCenterDbStore.Entities.AuditLog
        {
            Id = source.Id,
            UserId = source.UserId,
            ControllerName = source.ControllerName,
            ActionName = source.ActionName,
            ActionType = source.ActionType,
            RequestData = source.RequestData,
            ResponseData = source.ResponseData,
            ExecutionStatus = source.ExecutionStatus,
            ExecutionDate = source.ExecutionDate,
            ClientIp = source.ClientIp,
            Remarks = source.Remarks
        };
    }

    // Store Entity to Core Model
    public static Models.AuditLog ToCoreModel(this TransferCenterDbStore.Entities.AuditLog source)
    {
        if (source == null) return null!;

        return new Models.AuditLog
        {
            Id = source.Id,
            UserId = source.UserId,
            ControllerName = source.ControllerName,
            ActionName = source.ActionName,
            ActionType = source.ActionType,
            RequestData = source.RequestData,
            ResponseData = source.ResponseData,
            ExecutionStatus = source.ExecutionStatus,
            ExecutionDate = source.ExecutionDate,
            ClientIp = source.ClientIp,
            Remarks = source.Remarks
        };
    }

        
}