using TransferCenterCore.Interfaces;
using TransferCenterWeb.Models;
using TransferCenterWeb.Translators;

namespace TransferCenterWeb.Middleware;

public class AuditLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;

    public AuditLogMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _scopeFactory = scopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
            SaveAuditLog(context, "Success", string.Empty);
        }
        catch (Exception ex)
        {
            SaveAuditLog(context, "Failed", ex.Message);
            throw;
        }
    }

    private AuditLog GetAuditLog(HttpContext context)
    {
        return new AuditLog
        {
            UserId = context.Session.GetString(Constant.Session.UserId) ?? string.Empty,
            ControllerName = context.Request.RouteValues["controller"]?.ToString() ?? string.Empty,
            ActionName = context.Request.RouteValues["action"]?.ToString() ?? string.Empty,
            ActionType = context.Request.Method,
            ExecutionDate = DateTime.UtcNow,
            ClientIp = context.Connection.RemoteIpAddress?.ToString()
        };
    }

    private void SaveAuditLog(HttpContext context, string status, string remarks)
    {
        if (context.Request.Method == HttpMethods.Get) return;
        using var scope = _scopeFactory.CreateScope();
        var auditLog = GetAuditLog(context);
        var auditLogService = scope.ServiceProvider.GetRequiredService<IAuditLogService>();
        auditLog.ExecutionStatus = status;
        auditLog.Remarks = remarks;
        auditLog.UserId = context.Session.GetString(Constant.Session.UserId) ?? string.Empty;
        auditLogService.SaveAsync(auditLog.ToCoreModel());
    }
}

// Extension method for cleaner startup registration
public static class AuditLogMiddlewareExtensions
{
    public static IApplicationBuilder UseAuditLog(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuditLogMiddleware>();
    }
}