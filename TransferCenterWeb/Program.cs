using Microsoft.EntityFrameworkCore;
using TransferCenterCore.Interfaces;
using TransferCenterCore.Services;
using TransferCenterDbStore;
using TransferCenterDbStore.Data;
using TransferCenterDbStore.Interfaces;
using TransferCenterDbStore.Repositories;
using TransferCenterDbStore.UnitOfWork;
using TransferCenterWeb;
using TransferCenterWeb.Middleware;
using TransferCenterWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add authentication and authorization services
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

// Add authorization with policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireClaim("Role", "1")); // Role 1 is admin
});

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
builder.Services.AddScoped<IComorbiditiesAndRiskScoreRepository, ComorbiditiesAndRiskScoreRepository>();
builder.Services.AddScoped<IDbContextFactory, DbContextFactory>();

var buildNumber = builder.Configuration["BuildNumber"] ?? "Unknown";
var copyright = builder.Configuration["Copyright"] ?? "� 2025 Your Company";
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
