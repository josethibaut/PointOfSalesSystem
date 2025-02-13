namespace PointOfSalesSystem.Models
{
    public class CashierViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Sale CurrentSale { get; set; }
        public List<CartItem> CartItems { get; set; }
    }

}
