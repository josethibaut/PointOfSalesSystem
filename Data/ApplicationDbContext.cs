using Microsoft.EntityFrameworkCore;
using PointOfSalesSystem.Models;

namespace PointOfSalesSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<VatReturn> VatReturns { get; set; }  // ✅ Ensure this exists

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VatReturn>()
                .HasKey(v => v.VatReturnId);  // ✅ Explicitly set the primary key

            base.OnModelCreating(modelBuilder);
        }
    }
}
