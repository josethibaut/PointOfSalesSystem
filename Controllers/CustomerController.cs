using Microsoft.AspNetCore.Mvc;
using PointOfSalesSystem.Data;
using PointOfSalesSystem.Models;

namespace PointOfSalesSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult LoyaltyProgram()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        [HttpPost]
        public IActionResult RegisterCustomer(string customerName, bool isLoyaltyMember)
        {
            if (string.IsNullOrEmpty(customerName))
            {
                TempData["ErrorMessage"] = "Customer name is required.";
                return RedirectToAction("Index");
            }

            var customer = new Customer
            {
                Name = customerName,
                LoyaltyPoints = isLoyaltyMember ? 0 : 0 // Ensure it never gets null
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Customer registered successfully!";
            return RedirectToAction("Index");
        }

    }
}


