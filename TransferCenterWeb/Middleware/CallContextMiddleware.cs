using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TransferCenterCore.Context;
using TransferCenterWeb.Models;

namespace TransferCenterWeb.Middleware;

public class CallContextMiddleware
{
    private readonly RequestDelegate _next;

    public CallContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var callContext = new CallContext
        {
            CorrelationId = Activity.Current?.Id ?? httpContext.TraceIdentifier,
            IpAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
            UserName = httpContext.User.Identity?.Name,
            DisplayName = httpContext.User.FindFirstValue("name") ?? httpContext.User.Identity?.Name,
            EmailId = httpContext.User.FindFirstValue(ClaimTypes.Email),
            UserId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? httpContext.User.FindFirstValue("sub")
                ?? httpContext.Session.GetString(Constant.Session.UserId)
        };

        foreach (var roleClaim in httpContext.User.FindAll(ClaimTypes.Role))
        {
            if (!string.IsNullOrWhiteSpace(roleClaim.Value))
            {
                callContext.Roles.Add(roleClaim.Value);
            }
        }

        var sessionUserId = httpContext.Session.GetString(Constant.Session.UserId);
        if (!string.IsNullOrWhiteSpace(sessionUserId))
        {
            callContext.SetItem(Constant.Session.UserId, sessionUserId);
        }

        callContext.SetItem("RequestPath", httpContext.Request.Path);
        callContext.SetItem("RequestMethod", httpContext.Request.Method);

        using (CallContextScope.Begin(callContext))
        {
            await _next(httpContext);
        }
    }
}
