using Microsoft.AspNetCore.Mvc;
using PointOfSalesSystem.Models;

namespace PointOfSalesSystem.Controllers
{
    public class SalesController : Controller
    {
        // Display the sale processing form
        [HttpGet]
        public IActionResult ProcessSale()
        {
            var sale = new Sale();
            // Initialize sale properties if needed
            return View(sale);
        }

        // Handle form submission
        [HttpPost]
        public IActionResult ProcessSale(Sale sale)
        {
            if (ModelState.IsValid)
            {
                // Calculate VAT (assuming a 15% rate)
                sale.VATAmount = sale.TotalAmount * 0.15m;
                // Calculate change due
                sale.ChangeDue = sale.AmountReceived - (sale.TotalAmount + sale.VATAmount);

                // Save the sale to the database (implementation depends on your data access strategy)

                // Redirect to the receipt view
                return RedirectToAction("Receipt", new { id = sale.SaleId });
            }
            return View(sale);
        }

        // Display the receipt
        public IActionResult Receipt(int id)
        {
            // Retrieve the sale from the database using the id
            var sale = new Sale(); // Replace with actual retrieval logic

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }
    }
}
