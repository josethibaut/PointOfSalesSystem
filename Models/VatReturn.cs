using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSalesSystem.Models
{
    public class VatReturn
    {
        [Key]
        public int VatReturnId { get; set; }  // ✅ Ensure correct primary key

        [ForeignKey("Sale")]
        public int SaleId { get; set; }

        [Column("VATAmount", TypeName = "decimal(18,2)")]  // ✅ Ensure correct column mapping
        public decimal VATAmount { get; set; }

        public DateTime ReturnDate { get; set; }

        public Sale Sale { get; set; }  // ✅ Navigation Property
    }
}
