using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfSalesSystem.Data;
using PointOfSalesSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesSystem.Controllers
{
    public class VatReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VatReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vatReturns = await _context.VatReturns.ToListAsync();

            var vatReportModel = new VatReportModel
            {
                ReportName = "Monthly VAT Report",
                Amount = vatReturns.Sum(v => v.VATAmount), // Calculate total VAT
                VatReturns = vatReturns
            };

            return View(vatReportModel); // Pass the new model
        }
    }
}
