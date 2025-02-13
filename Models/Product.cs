using System.ComponentModel.DataAnnotations;

namespace PointOfSalesSystem.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Barcode { get; set; }  // ✅ Ensure this is correctly defined
    }
}
