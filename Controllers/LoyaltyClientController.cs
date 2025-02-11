using Microsoft.AspNetCore.Mvc;

namespace PointOfSalesSystem.Controllers
{
    public class LoyaltyClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
