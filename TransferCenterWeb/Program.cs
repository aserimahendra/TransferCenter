using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using TransferCenterCore.Interfaces;
using TransferCenterCore.Services;
using TransferCenterDbStore;
using TransferCenterDbStore.Data;
using TransferCenterDbStore.Interfaces;
using TransferCenterDbStore.Repositories;
using TransferCenterDbStore.UnitOfWork;
using TransferCenterHelper;
using TransferCenterWeb;
using TransferCenterWeb.Middleware;
using TransferCenterWeb.Models;
using TransferCenterHelper.Utility;

var builder = WebApplication.CreateBuilder(args);

// Configure appsettings.json to load environment-specific config
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName.ToLower()}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Conditional authentication: prefer Windows (Negotiate) if enabled, then Azure AD, else fallback to local cookie auth
var azureAdSection = builder.Configuration.GetSection("AzureAd");
var useWindowsAuth = builder.Configuration.GetValue<bool>("UseWindowsAuth");

if (useWindowsAuth)
{
    // Windows Integrated Authentication (local Active Directory)
    builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Negotiate.NegotiateDefaults.AuthenticationScheme)
        .AddNegotiate();

    builder.Services.AddAuthorization(options =>
    {
        // Require an authenticated Windows identity by default
        options.FallbackPolicy = options.DefaultPolicy;
    });
}
else if (azureAdSection.Exists() && !string.IsNullOrEmpty(azureAdSection["ClientId"]))
{
    // Azure AD (OpenID Connect)
    builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApp(azureAdSection);
    builder.Services.AddAuthorization(options =>
    {
        options.FallbackPolicy = options.DefaultPolicy;
    });
}
else
{
    // Local cookie-based login
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "TransferCenter";
        options.DefaultSignInScheme = "TransferCenter";
        options.DefaultChallengeScheme = "TransferCenter";
    })
    .AddCookie("TransferCenter", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Error/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdminRole", policy =>
            policy.RequireClaim("Role", "1")); // Role 1 is admin
    });
}

//  Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add DbContext
builder.Services.AddDbContext<BaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductivityToolConnection")));

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<IGlobalTransferService, GlobalTransferService>();
builder.Services.AddScoped<IPatientTransferService, PatientTransferService>();
builder.Services.AddScoped<IComorbiditiesAndRiskScoreRepository, ComorbiditiesAndRiskScoreRepository>();
builder.Services.AddScoped<IDbContextFactory, DbContextFactory>();
builder.Services.AddScoped<ITransferRequestRepository, TransferRequestRepository>();

// Ensure Playwright browser is installed (Chromium is required for PDF)
try { Microsoft.Playwright.Program.Main(new [] { "install", "chromium" }); } catch { /* ignore */ }
builder.Services.AddSingleton<IPdfExporter, PlaywrightPdfExporter>();
builder.Services.AddScoped<IViewRenderService, ViewRenderService>();

var buildNumber = builder.Configuration["BuildNumber"] ?? "Unknown";
var copyright = builder.Configuration["Copyright"] ?? "� 2025 KPC Group";
builder.Services.AddSingleton(new BuildInfo { BuildNumber = buildNumber, Copyright = copyright });



var app = builder.Build();

// Add exception handling middleware first in the pipeline
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseMiddleware<CallContextMiddleware>();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/", context => {
    if (context.Session.GetString("UserId") == null)
    {
        context.Response.Redirect("/Account/Login");
    }
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//app.UseAuditLog();
//app.UseExceptionHandlingMiddleware();
app.Run();
