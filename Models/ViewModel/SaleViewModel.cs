using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSalesSystem.Models.ViewModels
{
    public class SaleViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public decimal AmountReceived { get; set; }

        public decimal ChangeDue { get; set; }
        public string PaymentMethod { get; set; }

        public List<Customer> Customers { get; set; }
    }
}
