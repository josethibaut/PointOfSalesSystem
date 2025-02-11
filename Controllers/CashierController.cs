using Microsoft.AspNetCore.Mvc;

namespace PointOfSalesSystem.Controllers
{
    public class CashierController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
