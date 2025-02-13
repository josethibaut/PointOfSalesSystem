namespace PointOfSalesSystem.Models
{
    
    public class MonthlyVatReport
    {
        public string MonthYear { get; set; }  // Example: "January 2024"
        public decimal TotalVATAmount { get; set; } // Total VAT collected in the month
        public int TransactionCount { get; set; } // Number of VAT transactions
    }
}

