using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesSystem.Data;
using PointOfSalesSystem.Models;
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

        // ✅ Display Loyalty Registration Form
        public IActionResult Create()
        {
            return View();
        }

        // ✅ Process New Loyalty Member Registration
        [HttpPost]
        public async Task<IActionResult> Create(Customer model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Prevent duplicate entries
            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Name == model.Name);

            if (existingCustomer != null)
            {
                ModelState.AddModelError("Name", "Customer already exists.");
                return View(model);
            }

            model.LoyaltyPoints = 0; // Start with zero points
            _context.Customers.Add(model);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Loyalty member registered successfully!";
            return RedirectToAction("Index", "Cashier");
        }
    }
}
