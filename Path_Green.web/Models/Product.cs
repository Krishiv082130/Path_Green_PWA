using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        public string? UnitType { get; set; }
        public bool IsActive { get; set; } = true;

        public Inventory? Inventory { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
