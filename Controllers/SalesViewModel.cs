using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSalesSystem.Models.ViewModels
{
    public class SalesViewModel
    {
        public int SaleId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public decimal VATAmount { get; set; }

        [Required]
        public decimal AmountReceived { get; set; }

        public decimal ChangeDue { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public int AvailablePoints { get; set; } // For customer loyalty points

        public List<Customer> Customers { get; set; }
    }
}
