using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSalesSystem.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public decimal VATAmount { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
