using Microsoft.EntityFrameworkCore;
using PointOfSalesSystem.Models;

namespace PointOfSalesSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }   // ✅ Ensure Sale is here
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<VatReturn> VatReturns { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
