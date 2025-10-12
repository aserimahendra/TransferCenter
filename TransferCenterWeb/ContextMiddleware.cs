using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net;
using System.Threading.Tasks;
using TransferCenterWeb.Models;

namespace TransferCenterWeb
{


    public class ContextMiddleware
    {
        private readonly RequestDelegate _next;

        public ContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            // Allow these paths without requiring session
            string[] allowedPaths =
            {
            "/account/login",
            "/account/register",
            "/account/logout",
            "/favicon.ico",
            "/css", "/js", "/lib", "/images"
        };

            bool isAllowed = allowedPaths.Any(p => path.StartsWith(p)) || IsLoginAction(context) ||
                             context.Request.Path.Value.Contains("."); // static files

            var isAuthenticated = context.Session.GetString(Constant.Session.UserId) != null;

            if (!isAuthenticated && !isAllowed)
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            await _next(context);
        }

        private bool IsLoginAction(HttpContext context)
        {
            context.Request.RouteValues.TryGetValue("action", out var action);
            return action is not null && string.Equals(action.ToString(), "login", StringComparison.InvariantCultureIgnoreCase);
        }
    }

}
