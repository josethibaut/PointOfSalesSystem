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

        // 🛒 Show Products and Cart
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            ViewBag.CartItems = cartItems;

            // Retrieve the latest sale from the database
            var latestSale = await _context.Sales.OrderByDescending(s => s.SaleId).FirstOrDefaultAsync();
            ViewBag.Sale = latestSale;  // Pass the latest sale to the view

            return View(products);
        }




        // 🛍 Add item to Cart
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product == null) return NotFound();

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var existingItem = cartItems.FirstOrDefault(p => p.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            HttpContext.Session.SetObjectAsJson("Cart", cartItems);
            return RedirectToAction("Index");
        }

        // ❌ Remove item from Cart
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            cartItems.RemoveAll(p => p.ProductId == productId);

            HttpContext.Session.SetObjectAsJson("Cart", cartItems);
            return RedirectToAction("Index");
        }

        // 🏷️ Scan Barcode
        [HttpPost]
        public async Task<IActionResult> ScanBarcode(string barcode)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
            if (product == null) return NotFound();

            return RedirectToAction("AddToCart", new { productId = product.ProductId, quantity = 1 });
        }

        // 💰 Checkout Process
        [HttpPost]
        public async Task<IActionResult> Checkout(string paymentMethod)
        {
            if (string.IsNullOrEmpty(paymentMethod))
            {
                return BadRequest("Payment Method is required.");
            }

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (!cartItems.Any()) return BadRequest("Cart is empty.");

            var sale = new Sale
            {
                SaleDate = DateTime.Now,
                TotalAmount = cartItems.Sum(item => item.Price * item.Quantity),
                VATAmount = cartItems.Sum(item => item.Price * item.Quantity * 0.15m), // Assuming 15% VAT
                AmountReceived = 0, // Will be updated later
                ChangeDue = 0, // Will be updated later
                PaymentMethod = paymentMethod,
                SaleItems = cartItems.Select(c => new SaleItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Price
                }).ToList()
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart"); // Clear cart after purchase

            return RedirectToAction("Receipt", new { id = sale.SaleId });
        }

        // 🧾 Receipt Page
        public async Task<IActionResult> Receipt(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(s => s.SaleId == id);

            if (sale == null) return NotFound();

            return View(sale);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessVatReturn(int saleId)
        {
            // Find the sale record
            var sale = await _context.Sales.FindAsync(saleId);
            if (sale == null) return NotFound();

            // Check if VAT has already been refunded
            if (sale.VatReturned)
            {
                TempData["Error"] = "VAT return has already been processed for this sale.";
                return RedirectToAction("Index");
            }

            // Create VAT return record
            var vatReturn = new VatReturn
            {
                SaleId = sale.SaleId,
                VATAmount = sale.VATAmount,
                ReturnDate = DateTime.Now
            };

            // Save VAT return to database
            _context.VatReturns.Add(vatReturn);
            sale.VatReturned = true; // Update sale record to prevent duplicate VAT returns
            await _context.SaveChangesAsync();

            TempData["Success"] = "VAT return processed successfully!";
            return RedirectToAction("Index");
        }

    }
}
   
