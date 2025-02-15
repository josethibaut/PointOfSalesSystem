using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PointOfSalesSystem.Data;
using PointOfSalesSystem.Models;

namespace PointOfSalesSystem.Controllers
{
    public class VatReturnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VatReturnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Index - Default VAT Returns View
        public async Task<IActionResult> Index()
        {
            var vatReturns = await _context.VatReturns.ToListAsync();
            return View(vatReturns);
        }

        // ✅ Monthly VAT Report - Groups data by month
        public async Task<IActionResult> MonthlyReport()
        {
            var monthlyVatData = await _context.VatReturns
                .GroupBy(v => new { Year = v.ReturnDate.Year, Month = v.ReturnDate.Month })
                .Select(g => new MonthlyVatReport
                {
                    MonthYear = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy", CultureInfo.InvariantCulture),
                    TotalVATAmount = g.Sum(v => v.VATAmount),
                    TransactionCount = g.Count()
                })
                .OrderByDescending(r => r.MonthYear)
                .ToListAsync();

            return View(monthlyVatData);
        }
    }
}
