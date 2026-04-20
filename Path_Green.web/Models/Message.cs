using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Path_Green.web.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? PostedByUserID { get; set; }
        public IdentityUser? Sender { get; set; }
        public DateTime PostedAt { get; set; }
        public bool IsActive { get; set; }

        public string? UserID { get; set; }
        public IdentityUser? User { get; set; }
    }
}
