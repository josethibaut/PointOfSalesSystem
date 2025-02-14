using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSalesSystem.Models
{
    public class SaleItem
    {
        [Key]
        public int SaleItemId { get; set; }

        [ForeignKey("Sale")]
        public int SaleId { get; set; }
        public Sale Sale { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
