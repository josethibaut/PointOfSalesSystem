using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSalesSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        public int LoyaltyPoints { get; set; } = 0;

        public List<LoyaltyTransaction> LoyaltyTransactions { get; set; } = new List<LoyaltyTransaction>();
    }
}
