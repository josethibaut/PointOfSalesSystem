using PointOfSalesSystem.Models;

namespace PointOfSalesSystem.Controllers
{
    internal class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}