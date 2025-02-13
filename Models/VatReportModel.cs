using System.Collections.Generic;

namespace PointOfSalesSystem.Models
{
    public class VatReportModel
    {
        public string ReportName { get; set; }  // Name of the report
        public decimal Amount { get; set; }  // Total VAT amount
        public List<VatReturn> VatReturns { get; set; }  // List of VAT returns
    }
}
