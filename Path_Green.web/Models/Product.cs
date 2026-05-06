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

        public string? Category { get; set; }

        public string? SubCategory { get; set; }

        public Inventory? Inventory { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
