using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PointOfSalesSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Configure Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// ✅ Configure Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ✅ Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// ✅ Define Routing
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "loyalty", pattern: "Loyalty/{action=Create}/{id?}", defaults: new { controller = "Loyalty", action = "Create" });
app.MapControllerRoute(name: "add-to-cart", pattern: "Cashier/AddToCart", defaults: new { controller = "Cashier", action = "AddToCart" });
app.MapControllerRoute(name: "receipt", pattern: "Cashier/Receipt/{id?}", defaults: new { controller = "Cashier", action = "Receipt" });

app.MapRazorPages();
app.Run();
