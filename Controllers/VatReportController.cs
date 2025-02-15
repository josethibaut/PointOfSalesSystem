using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesSystem.Data;
using PointOfSalesSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesSystem.Controllers
{
    public class VatReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VatReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            // Default to current month if no date is provided
            if (!startDate.HasValue) startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (!endDate.HasValue) endDate = DateTime.Now;

            // Fetch VAT collected from sales
            var vatOnSales = await _context.Sales
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .SumAsync(s => s.VATAmount);

            // Fetch VAT paid on purchases
            var vatOnPurchases = await _context.Purchases
                .Where(p => p.PurchaseDate >= startDate && p.PurchaseDate <= endDate)
                .SumAsync(p => p.VATAmount);

            // Fetch VAT returns (refunds)
            var vatOnReturns = await _context.VatReturns
                .Where(v => v.ReturnDate >= startDate && v.ReturnDate <= endDate)
                .SumAsync(v => v.VATAmount);

            // Calculate Net VAT Payable
            decimal netVatPayable = vatOnSales - vatOnReturns - vatOnPurchases;

            // Prepare View Model
            var vatReportModel = new VatReportModel
            {
                ReportName = "Monthly VAT Report",
                Amount = netVatPayable,
                VatReturns = await _context.VatReturns
                    .Where(v => v.ReturnDate >= startDate && v.ReturnDate <= endDate)
                    .ToListAsync(),
                StartDate = startDate.Value,
                EndDate = endDate.Value
            };

            return View(vatReportModel);
        }
    }
}
