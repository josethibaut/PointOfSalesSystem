using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PointOfSalesSystem.Data; // Fixed namespace
using PointOfSalesSystem.Services;
using PointOfSalesSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Stripe;



namespace PointOfSalesSystem // Fixed namespace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<VatService>();
            services.AddScoped<StockService>();
            services.AddScoped<LoyaltyService>();
            services.AddScoped<AuditLogService>();

            services.AddScoped<ProductService>();
            services.AddScoped<SaleService>();
            services.AddScoped<PaymentService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<SupplierService>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<UserManager<IdentityUser>>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSession();
            // Add services to the container.
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }

    public class VatService
    {
        public decimal CalculateVAT(decimal price, decimal rate)
        {
            return price * (rate / 100);
        }
    }

    public class StockService
    {
        public int CalculateStockAfterSale(int initialStock, int quantitySold)
        {
            return initialStock - quantitySold;
        }
    }

    public class LoyaltyService
    {
        public int CalculateLoyaltyPoints(decimal purchaseAmount)
        {
            return (int)(purchaseAmount / 100);
        }
    }
}
