using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Path_Green.web.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int OrderStatusID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        public bool IsFirstTimeOrder { get; set; }
        public string? Notes { get; set; }

        public string? UserID { get; set; }
        public IdentityUser? User { get; set; }
        public OrderStatus? OrderStatus { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<SurveyResponse>? SurveyResponses { get; set; }
    }
}
