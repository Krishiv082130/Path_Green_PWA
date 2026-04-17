using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Path_Green.web.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public int PostedByUserID { get; set; }
        public DateTime PostedAt { get; set; }
        public bool IsActive { get; set; }

        public User? Sender { get; set; }
    }
}
