using System.ComponentModel.DataAnnotations;

namespace PointOfSalesSystem.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }  // ✅ Ensure this exists

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }  // ✅ Ensure this exists
    }
}
