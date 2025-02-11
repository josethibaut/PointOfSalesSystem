using Microsoft.EntityFrameworkCore;
using PointOfSalesSystem.Models; // Add this line

namespace PointOfSalesSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }  // Ensure these models exist
        public DbSet<Customer> Customers { get; set; }
    }
}
