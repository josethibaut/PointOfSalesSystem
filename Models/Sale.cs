using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSalesSystem.Models
{
    public class Sale
    {
        internal bool VatReturned;

        [Key]
        public int SaleId { get; set; }

        public DateTime SaleDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public decimal VATAmount { get; set; }

        [Required]
        public decimal AmountReceived { get; set; }

        [Required]
        public decimal ChangeDue { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        // 🏷️ Relationship: Sale contains multiple SaleItems
        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
