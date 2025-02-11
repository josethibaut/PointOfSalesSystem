namespace PointOfSalesSystem.Models
{
    public class SalesViewModel
    {
        public decimal TotalAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal ChangeDue { get; set; }
        public string PaymentMethod { get; set; }
    }
}
