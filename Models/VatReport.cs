namespace PointOfSalesSystem.Models
{
    public class VatReport
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
