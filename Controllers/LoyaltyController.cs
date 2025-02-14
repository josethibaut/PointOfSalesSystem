using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesSystem.Data;
using PointOfSalesSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesSystem.Controllers
{
    public class LoyaltyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoyaltyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers
                .Include(c => c.LoyaltyTransactions)
                .ToListAsync();

            return View(customers);
        }

        public async Task<IActionResult> LoyaltyDetails(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.LoyaltyTransactions)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
    }
}
