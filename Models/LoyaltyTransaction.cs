using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSalesSystem.Models
{
    public class LoyaltyTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Sale")]
        public int? SaleId { get; set; }  // Sale ID is optional

        public int PointsEarned { get; set; }
        public int PointsRedeemed { get; set; } = 0;
        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}
