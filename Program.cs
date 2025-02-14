using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PointOfSalesSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Identity & Authentication Setup
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ✅ Add Controllers, Views, Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// ✅ Configure Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ✅ Add HTTP Context Accessor (for session)
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ✅ Middleware Setup
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// ✅ Enforce HTTPS & Static Files
app.UseHttpsRedirection();
app.UseStaticFiles();

// ✅ Enable Authentication & Session Middleware
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// ✅ Define Routing Rules
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "vat-monthly",
    pattern: "VatReturns/MonthlyReport",
    defaults: new { controller = "VatReturns", action = "MonthlyReport" }
);

app.MapControllerRoute(
    name: "customer-loyalty",
    pattern: "Customers/LoyaltyPoints/{id?}",
    defaults: new { controller = "Customers", action = "LoyaltyPoints" }
);

app.MapRazorPages();
app.Run();
