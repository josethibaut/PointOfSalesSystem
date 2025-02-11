namespace PointOfSalesSystem.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VATAmount { get; set; }  // Add this property
        public decimal AmountReceived { get; set; }  // Add this property
        public decimal ChangeDue { get; set; }  // Add this property
        public string PaymentMethod { get; set; }  // Add this property
    }
}
