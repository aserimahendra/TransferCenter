using Microsoft.EntityFrameworkCore;
using TransferCenterCore.Interface;
using TransferCenterCore.Service;
using TransferCenterDbStore.Data;
using TransferCenterDbStore.UnitOfWork;
using TransferCenterWeb;
using TransferCenterWeb.Models; // Required for session

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session services
builder.Services.AddDistributedMemoryCache();
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
builder.Services.AddScoped<IGlobalTransferService, GlobalTransferService>();

var buildNumber = builder.Configuration["BuildNumber"] ?? "Unknown";
var copyright = builder.Configuration["Copyright"] ?? "© 2025 Your Company";
builder.Services.AddSingleton(new BuildInfo { BuildNumber = buildNumber, Copyright = copyright });



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Enable session middleware
app.UseMiddleware<ContextMiddleware>();
app.UseAuthorization();

app.MapControllers();

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

app.Run();
