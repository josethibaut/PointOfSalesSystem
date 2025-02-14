using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesSystem.Data;
using PointOfSalesSystem.Models;
using PointOfSalesSystem.Models.ViewModels;
using System.Linq;

namespace PointOfSalesSystem.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display Sale Processing Form
        [HttpGet]
        public IActionResult ProcessSale()
        {
            var viewModel = new SalesViewModel
            {
                Customers = _context.Customers.ToList()
            };
            return View(viewModel);
        }

        // Process Sale
        [HttpPost]
        public IActionResult ProcessSale(SalesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == viewModel.CustomerId);
                if (customer == null)
                {
                    return NotFound("Customer not found.");
                }

                // Calculate VAT
                decimal vatRate = 0.15m;
                decimal vatAmount = viewModel.TotalAmount * vatRate;
                decimal totalWithVAT = viewModel.TotalAmount + vatAmount;
                decimal changeDue = viewModel.AmountReceived - totalWithVAT;

                // Save Sale
                var sale = new Sale
                {
                    CustomerId = viewModel.CustomerId,
                    TotalAmount = viewModel.TotalAmount,
                    VATAmount = vatAmount,
                    AmountReceived = viewModel.AmountReceived,
                    ChangeDue = changeDue,
                    PaymentMethod = viewModel.PaymentMethod,
                    SaleDate = DateTime.Now
                };

                _context.Sales.Add(sale);
                _context.SaveChanges();

                // Earn Loyalty Points
                int pointsEarned = (int)(viewModel.TotalAmount / 10);
                customer.LoyaltyPoints += pointsEarned;

                _context.LoyaltyTransactions.Add(new LoyaltyTransaction
                {
                    CustomerId = customer.CustomerId,
                    SaleId = sale.SaleId,
                    PointsEarned = pointsEarned,
                    TransactionDate = System.DateTime.Now
                });

                _context.SaveChanges();
                return RedirectToAction("Receipt", new { id = sale.SaleId });
            }

            viewModel.Customers = _context.Customers.ToList();
            return View(viewModel);
        }

        // Display Receipt
        public IActionResult Receipt(int id)
        {
            var sale = _context.Sales.Include(s => s.Customer).FirstOrDefault(s => s.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }
    }
}
