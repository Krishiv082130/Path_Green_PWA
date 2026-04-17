using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class Donation
    {
        public int DonationID { get; set; }
        public int UserID { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public DateTime DonationDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }

        public User? User { get; set; }
    }
}
