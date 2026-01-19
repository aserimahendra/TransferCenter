using System;
using TransferCenterCore.Models;

namespace TransferCenterWeb.Translators;
public static class AuditLogTranslator
{

// Web Model to Core Model
        public static AuditLog ToCoreModel(this Models.AuditLog source)
        {
            if (source == null) return null!;

            return new AuditLog
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

        // Core Model to Web Model
        public static Models.AuditLog ToWebModel(this Models.AuditLog source)
        {
            if (source == null) return null!;

            return new TransferCenterWeb.Models.AuditLog
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
