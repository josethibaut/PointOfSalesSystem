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

        // ✅ Show Cashier Desk with Customer Selection
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var customers = await _context.Customers.ToListAsync();
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Retrieve selected customer and sale ID from session
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            int? saleId = HttpContext.Session.GetInt32("SaleId");

            Customer customer = customerId.HasValue
                ? await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId)
                : null;

            Sale sale = saleId.HasValue
                ? await _context.Sales.FirstOrDefaultAsync(s => s.SaleId == saleId)
                : null;

            ViewBag.CartItems = cartItems;
            ViewBag.Customers = customers;
            ViewBag.SelectedCustomer = customer;
            ViewBag.SaleId = sale?.SaleId;

            return View(products);
        }

        // ✅ Select Customer & Generate SalesId
        [HttpPost]
        public IActionResult SetCustomer(int customerId)
        {
            if (customerId == 0)
            {
                TempData["ErrorMessage"] = "Please select a customer.";
                return RedirectToAction("Index");
            }

            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Customer not found.";
                return RedirectToAction("Index");
            }

            var newSale = new Sale
            {
                CustomerId = customerId,
                SaleDate = DateTime.Now,
                TotalAmount = 0,
                VATAmount = 0,
                AmountReceived = 0,
                ChangeDue = 0,
                PaymentMethod = "N/A"
            };

            _context.Sales.Add(newSale);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("CustomerId", customerId);
            HttpContext.Session.SetInt32("SaleId", newSale.SaleId);

            return RedirectToAction("Index");
        }

        // ✅ Process VAT Return
        [HttpPost]
        public IActionResult ProcessVatReturn(int saleId)
        {
            var sale = _context.Sales.FirstOrDefault(s => s.SaleId == saleId);
            if (sale == null)
            {
                TempData["ErrorMessage"] = "Sale not found for VAT return.";
                return RedirectToAction("Index");
            }

            var vatReturn = new VatReturn
            {
                SaleId = sale.SaleId,
                ReturnDate = DateTime.Now,
                VATAmount = sale.VATAmount
            };

            _context.VatReturns.Add(vatReturn);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ✅ Complete Sale
        [HttpPost]
        // ✅ Complete Sale
        [HttpPost]
        public IActionResult CompleteSale(int customerId, string paymentMethod)
        {
            // ✅ Provide the key "Cart" when retrieving session data
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart")
                            ?? new List<CartItem>();

            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Cart is empty.";
                return RedirectToAction("Index");
            }

            var totalAmount = cartItems.Sum(item => item.Price * item.Quantity);
            decimal vatAmount = totalAmount * 0.15m;
            decimal amountReceived = totalAmount + vatAmount;

            var sale = new Sale
            {
                CustomerId = customerId,
                TotalAmount = totalAmount,
                VATAmount = vatAmount,
                AmountReceived = amountReceived,
                ChangeDue = 0,
                PaymentMethod = paymentMethod,
                SaleDate = DateTime.Now
            };

            _context.Sales.Add(sale);
            _context.SaveChanges();

            // ✅ Save the updated (empty) cart in the session
            HttpContext.Session.SetObjectAsJson("Cart", new List<CartItem>());

            return RedirectToAction("Receipt", new { id = sale.SaleId });
        }


        // ✅ Display Receipt
        public IActionResult Receipt(int id)
        {
            var sale = _context.Sales.Include(s => s.Customer).FirstOrDefault(s => s.SaleId == id);

            if (sale == null)
            {
                TempData["ErrorMessage"] = "Sale not found.";
                return RedirectToAction("Index");
            }

            return View(sale);
        }
    }
}
