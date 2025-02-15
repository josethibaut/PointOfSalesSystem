using System.Collections.Generic;

namespace PointOfSalesSystem.Models
{
    public class CheckoutViewModel
    {
        public int CustomerId { get; set; } // Ensure it's in your model
        public List<CartItem> CartItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
