using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class OrderStatus
    {
        public int OrderStatusID { get; set; }
        public string? StatusName { get; set; }
        public string? Description { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
