using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSalesSystem.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal ChangeDue { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.Now;

        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
