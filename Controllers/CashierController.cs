using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesSystem.Data;
using PointOfSalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesSystem.Controllers
{
    public class CashierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CashierController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show Cashier Desk
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            ViewBag.CartItems = cartItems;

            // Retrieve the latest sale from the database
            var latestSale = await _context.Sales.OrderByDescending(s => s.SaleId).FirstOrDefaultAsync();

            // Handle NULL values
            if (latestSale == null)
            {
                ViewBag.Sale = new Sale
                {
                    SaleId = 0,
                    CustomerId = 0,
                    TotalAmount = 0,
                    VATAmount = 0,
                    AmountReceived = 0,
                    ChangeDue = 0,
                    PaymentMethod = "N/A",
                    SaleDate = DateTime.Now
                };
            }
            else
            {
                ViewBag.Sale = latestSale;
            }

            return View(products);
        }
    }
}
