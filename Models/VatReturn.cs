using System;

namespace PointOfSalesSystem.Models
{
    public class VatReturn
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public decimal VATAmount { get; set; }
        public DateTime ReturnDate { get; set; }

        public Sale Sale { get; set; }  // Foreign key relationship
    }
}
