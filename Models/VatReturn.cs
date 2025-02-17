﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSalesSystem.Models
{
    public class VatReturn
    {
        [Key]
        public int VatReturnId { get; set; }

        [ForeignKey("Sale")]
        public int SaleId { get; set; }
        public Sale Sale { get; set; }

        public decimal VATAmount { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; } = DateTime.Now;
    }
}
